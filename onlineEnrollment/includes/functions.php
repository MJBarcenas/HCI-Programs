<?php
function updateStudentNum() {
    $file = fopen("..\Registration\Registration\studentNum.txt", "r");
    $Num = fread($file, filesize("..\Registration\Registration\studentNum.txt")) + 1;
    $file = fopen("..\Registration\Registration\studentNum.txt", "w");
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

function insert($fname, $lname, $mname, $add, $email, $num, $ispaid) {
    $conn = new mysqli("localhost", "root", "", "onlineenrollment");

    if ($conn -> connect_errno) {
        echo "<script>alert('Failed to connect to database!')</script>";
        exit();
    } 

    $studentNum = generateStudentNum();
    $password = generatePassword();

    $sql = "INSERT INTO students (studentnum, firstname, lastname, middlename, address, email, number, ispaid, accpass) VALUES('{$studentNum}', '{$fname}', '{$lname}', '{$mname}', '{$add}', '{$email}', '{$num}', $ispaid, '{$password}');";
    $conn->query($sql);
    $conn->close();

    echo "<script>alert('Enrolled Successfully!')</script>";
}
?>