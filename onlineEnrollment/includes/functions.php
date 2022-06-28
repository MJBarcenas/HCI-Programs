<?php
function updateStudentNum() {
    $file = fopen("..\studentNum.txt", "r");
    $Num = fread($file, filesize("..\studentNum.txt")) + 1;
    $file = fopen("..\studentNum.txt", "w");
    fwrite($file, $Num);
    fclose($file);

    return $Num;
}

function generateStudentNum() {
    $date = substr(date('Y'), 2);
    $num = updateStudentNum();
    $zeros = str_repeat("0", 5 - strlen(strval($num)));
    $studentNum = "{$date}-{$zeros}{$num}";
    return $studentNum;
}

function generatePassword() {
    $alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    $num = "0123456789";
    $password = "";
    
    for ($i = 0; $i < 8; $i++) {
        $random = rand(0, 1);
        if ($random == 0) {
            $add = substr($alpha, rand(0, 25), 1);
        } else {
            $add = substr($num, rand(0, 9), 1);
        }
        $password = "{$password}{$add}";
    }

    return $password;
}

function insert($fname, $lname, $mname, $add, $email, $num, $ispaid, $course, $gender, $year='1') {
    $conn = new mysqli("localhost", "root", "", "onlineenrollment");

    if ($conn -> connect_errno) {
        echo "<script>alert('Failed to connect to database!')</script>";
        exit();
    } 

    $studentNum = generateStudentNum();
    $password = generatePassword();

    $sql = "SELECT studentnum FROM students WHERE (firstname='{$fname}' AND lastname='{$lname}') AND email='{$email}'";
    $result = $conn->query($sql);
    $result = $result->fetch_array(MYSQLI_NUM);

    try {
        if (count($result) > 0) {
            echo "<script>alert('You have already enrolled!')</script>";
        }
    } catch (\Throwable $th) {
        $sql = "INSERT INTO students (studentnum, firstname, lastname, middlename, address, email, number, ispaid, year, accpass, course, gender) VALUES('{$studentNum}', '{$fname}', '{$lname}', '{$mname}', '{$add}', '{$email}', '{$num}', $ispaid, '{$year}', '{$password}', '{$course}', '{$gender}');";
        $conn->query($sql);
        $conn->close();

        echo "<script>alert('Enrolled Successfully!')</script>";
    } 
}

function rrmdir($src) {
    $dir = opendir($src);
    while(false !== ( $file = readdir($dir)) ) {
        if (( $file != '.' ) && ( $file != '..' )) {
            $full = $src . '/' . $file;
            if ( is_dir($full) ) {
                rrmdir($full);
            }
            else {
                unlink($full);
            }
        }
    }
    closedir($dir);
    rmdir($src);
}

function update($fname, $lname, $mname, $add, $email, $num, $ispaid, $sNum) {
    if (strtoupper($mname) == "NONE") {
        $mname = "";
    }
    $conn = new mysqli("localhost", "root", "", "onlineenrollment");

    if ($conn -> connect_errno) {
        echo "<script>alert('Failed to connect to database!')</script>";
        exit();
    } 

    $studentNum = generateStudentNum();
    $password = generatePassword();

    $sql = "SELECT year FROM students WHERE (firstname='{$fname}' AND lastname='{$lname}') AND (ispaid=1 AND section!='')";
    $result = $conn->query($sql);
    $result = $result->fetch_array(MYSQLI_NUM);

    try {
        if (count($result) > 0) {
            $year = $result[0] + 1;
            if ($year == 2) {
                if ($mname == "") {
                    if (file_exists("D:/Programming/Programs/uploads/Freshmen/{$lname}, {$fname}")) {
                        rrmdir("D:/Programming/Programs/uploads/Freshmen/{$lname}, {$fname}");
                    }
                    if (file_exists("D:/Programming/Programs/uploads/Transferee/{$lname}, {$fname}")) {
                        rrmdir("D:/Programming/Programs/uploads/Transferee/{$lname}, {$fname}");
                    }
                } else {
                    if (file_exists("D:/Programming/Programs/uploads/Freshmen/{$lname}, {$fname}-{$mname}")) {
                        rrmdir("D:/Programming/Programs/uploads/Freshmen/{$lname}, {$fname}-{$mname}");
                    }
                    if (file_exists("D:/Programming/Programs/uploads/Transferee/{$lname}, {$fname}-{$mname}")) {
                        rrmdir("D:/Programming/Programs/uploads/Transferee/{$lname}, {$fname}-{$mname}");
                    }
                }
            } elseif ($year == 5) {
                echo "<script>alert('You have already graduated')</script>";
                return;
            } else {
                if ($mname == "") {
                    if (file_exists("D:/Programming/Programs/uploads/2nd-4th Year/{$lname}, {$fname}")) {
                        rrmdir("D:/Programming/Programs/uploads/2nd-4th Year/{$lname}, {$fname}");
                    }
                    if (file_exists("D:/Programming/Programs/uploads/Transferee/{$lname}, {$fname}")) {
                        rrmdir("D:/Programming/Programs/uploads/Transferee/{$lname}, {$fname}");
                    }
                } else {
                    if (file_exists("D:/Programming/Programs/uploads/2nd-4th Year/{$lname}, {$fname}-{$mname}")) {
                        rrmdir("D:/Programming/Programs/uploads/2nd-4th Year/{$lname}, {$fname}-{$mname}");
                    }
                    if (file_exists("D:/Programming/Programs/uploads/Transferee/{$lname}, {$fname}-{$mname}")) {
                        rrmdir("D:/Programming/Programs/uploads/Transferee/{$lname}, {$fname}-{$mname}");
                    }
                }
            }
            $sql = "UPDATE students SET studentnum='{$studentNum}', firstname='{$fname}', lastname='{$lname}', middlename='{$mname}', address='{$add}', email='{$email}', number='{$num}', ispaid='{$ispaid}', year='{$year}', section='', accpass='{$password}' WHERE studentnum='{$sNum}';";
            $conn->query($sql);
            $conn->close();

            echo "<script>alert('Enrolled Successfully!')</script>";
        }
    } catch (\Throwable $th) {
        echo "<script>alert('You are not a student of Colegio de Montalban')</script>";
    } 
}
?>