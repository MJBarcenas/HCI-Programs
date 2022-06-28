<?php
    include_once 'includes/functions.php';

    if (isset($_POST['Fresh'])) {
        $fname = $_POST['Ffname'];
        $lname = $_POST['Flname'];
        $mname = $_POST['Fmname'];
        $email = $_POST['Femail'];
        $no = $_POST['Fno'];
        $add = $_POST['Fadd'];
        $gender = $_POST['Fgender'];
        $course = $_POST['Fcourse'];
    
        $file = $_FILES['FPfile'];
    
        $fileName = $_FILES['FPfile']['name'];
        $fileType = $_FILES['FPfile']['type'];
        $fileSize = $_FILES['FPfile']['size'];
        $fileError = $_FILES['FPfile']['error'];
        $fileTMP = $_FILES['FPfile']['tmp_name'];
    
        $fileExt = explode('.', $fileName);
        $fileAExt = strtolower(end($fileExt));
    
        $file1 = $_FILES['FBfile'];
    
        $fileName1 = $_FILES['FBfile']['name'];
        $fileType1 = $_FILES['FBfile']['type'];
        $fileSize1 = $_FILES['FBfile']['size'];
        $fileError1 = $_FILES['FBfile']['error'];
        $fileTMP1 = $_FILES['FBfile']['tmp_name'];
    
        $fileExt1 = explode('.', $fileName1);
        $fileAExt1 = strtolower(end($fileExt1));

        $file2 = $_FILES['FMfile'];
    
        $fileName2 = $_FILES['FMfile']['name'];
        $fileType2 = $_FILES['FMfile']['type'];
        $fileSize2 = $_FILES['FMfile']['size'];
        $fileError2 = $_FILES['FMfile']['error'];
        $fileTMP2 = $_FILES['FMfile']['tmp_name'];
    
        $fileExt2 = explode('.', $fileName2);
        $fileAExt2 = strtolower(end($fileExt2));

        $file3 = $_FILES['FFfile'];
    
        $fileName3 = $_FILES['FFfile']['name'];
        $fileType3 = $_FILES['FFfile']['type'];
        $fileSize3 = $_FILES['FFfile']['size'];
        $fileError3 = $_FILES['FFfile']['error'];
        $fileTMP3 = $_FILES['FFfile']['tmp_name'];
    
        $fileExt3 = explode('.', $fileName3);
        $fileAExt3 = strtolower(end($fileExt3));

        if (strtoupper($mname) == "NONE") {
            $path = "D:/Programming/Programs/uploads/Freshmen/{$lname}, {$fname}";
            mkdir($path);
        } else {
            $path = "D:/Programming/Programs/uploads/Freshmen/{$lname}, {$fname}-{$mname}";
            mkdir($path);
        }

        move_uploaded_file($fileTMP, "{$path}/[PICTURE] {$lname}, {$fname}.{$fileAExt}");
        move_uploaded_file($fileTMP1, "{$path}/[BIRTH CERTIFICATE] {$lname}, {$fname}.{$fileAExt1}");
        move_uploaded_file($fileTMP2, "{$path}/[GOOD MORAL] {$lname}, {$fname}.{$fileAExt2}");
        move_uploaded_file($fileTMP3, "{$path}/[FORM 138] {$lname}, {$fname}.{$fileAExt3}");
        insert($fname, $lname, $mname, $add, $email, $no, 0, $course, $gender);
        
    
    } elseif (isset($_POST['Y24'])) {
        $fname = $_POST['Y24fname'];
        $lname = $_POST['Y24lname'];
        $mname = $_POST['Y24mname'];
        $email = $_POST['Y24email'];
        $no = $_POST['Y24no'];
        $add = $_POST['Y24add'];
        $sNum = $_POST['sNum'];

        $file = $_FILES['Y24Ofile'];

        $fileName = $_FILES['Y24Ofile']['name'];
        $fileType = $_FILES['Y24Ofile']['type'];
        $fileSize = $_FILES['Y24Ofile']['size'];
        $fileError = $_FILES['Y24Ofile']['error'];
        $fileTMP = $_FILES['Y24Ofile']['tmp_name'];

        $fileExt = explode('.', $fileName);
        $fileAExt = strtolower(end($fileExt));

        $file1 = $_FILES['Y24Sfile'];

        $fileName1 = $_FILES['Y24Sfile']['name'];
        $fileType1 = $_FILES['Y24Sfile']['type'];
        $fileSize1 = $_FILES['Y24Sfile']['size'];
        $fileError1 = $_FILES['Y24Sfile']['error'];
        $fileTMP1 = $_FILES['Y24Sfile']['tmp_name'];

        $fileExt1 = explode('.', $fileName1);
        $fileAExt1 = strtolower(end($fileExt1));

        update($fname, $lname, $mname, $add, $email, $no, 0, $sNum);

        if (strtoupper($mname) == "NONE") {
            $path = "D:/Programming/Programs/uploads/2nd-4th Year/{$lname}, {$fname}";
            mkdir($path);
        } else {
            $path = "D:/Programming/Programs/uploads/2nd-4th Year/{$lname}, {$fname}-{$mname}";
            mkdir($path);
        }

        move_uploaded_file($fileTMP, "{$path}/[PICTURE] {$lname}, {$fname}.{$fileAExt}");
        move_uploaded_file($fileTMP1, "{$path}/[BIRTH CERTIFICATE] {$lname}, {$fname}.{$fileAExt1}");
        
        
    } elseif (isset($_POST['Trans'])) {
        $fname = $_POST['Tfname'];
        $lname = $_POST['Tlname'];
        $mname = $_POST['Tmname'];
        $email = $_POST['Temail'];
        $no = $_POST['Tno'];
        $add = $_POST['Tadd'];
        $gender = $_POST['Tgender'];
        $course = $_POST['Tcourse'];
        $year = $_POST['Tyear'];

        $file = $_FILES['TPfile'];

        $fileName = $_FILES['TPfile']['name'];
        $fileType = $_FILES['TPfile']['type'];
        $fileSize = $_FILES['TPfile']['size'];
        $fileError = $_FILES['TPfile']['error'];
        $fileTMP = $_FILES['TPfile']['tmp_name'];

        $fileExt = explode('.', $fileName);
        $fileAExt = strtolower(end($fileExt));

        $file1 = $_FILES['TBfile'];

        $fileName1 = $_FILES['TBfile']['name'];
        $fileType1 = $_FILES['TBfile']['type'];
        $fileSize1 = $_FILES['TBfile']['size'];
        $fileError1 = $_FILES['TBfile']['error'];
        $fileTMP1 = $_FILES['TBfile']['tmp_name'];
        
        $fileExt1 = explode('.', $fileName1);
        $fileAExt1 = strtolower(end($fileExt1));

        $file2 = $_FILES['TTfile'];

        $fileName2 = $_FILES['TTfile']['name'];
        $fileType2 = $_FILES['TTfile']['type'];
        $fileSize2 = $_FILES['TTfile']['size'];
        $fileError2 = $_FILES['TTfile']['error'];
        $fileTMP2 = $_FILES['TTfile']['tmp_name'];

        $fileExt2 = explode('.', $fileName2);
        $fileAExt2 = strtolower(end($fileExt2));

        $file3 = $_FILES['THfile'];

        $fileName3 = $_FILES['THfile']['name'];
        $fileType3 = $_FILES['THfile']['type'];
        $fileSize3 = $_FILES['THfile']['size'];
        $fileError3 = $_FILES['THfile']['error'];
        $fileTMP3 = $_FILES['THfile']['tmp_name'];
        
        $fileExt3 = explode('.', $fileName3);
        $fileAExt3 = strtolower(end($fileExt3));

        $file4 = $_FILES['TGfile'];

        $fileName4 = $_FILES['TGfile']['name'];
        $fileType4 = $_FILES['TGfile']['type'];
        $fileSize4 = $_FILES['TGfile']['size'];
        $fileError4 = $_FILES['TGfile']['error'];
        $fileTMP4 = $_FILES['TGfile']['tmp_name'];
        
        $fileExt4 = explode('.', $fileName4);
        $fileAExt4 = strtolower(end($fileExt4));

        if (strtoupper($mname) == "NONE") {
            $path = "D:/Programming/Programs/uploads/Transferee/{$lname}, {$fname}";
            mkdir($path);
        } else {
            $path = "D:/Programming/Programs/uploads/Transferee/{$lname}, {$fname}-{$mname}";
            mkdir($path);
        }

        move_uploaded_file($fileTMP, "{$path}/[PICTURE] {$lname}, {$fname}.{$fileAExt}");
        move_uploaded_file($fileTMP1, "{$path}/[BIRTH CERTIFICATE] {$lname}, {$fname}.{$fileAExt1}");
        move_uploaded_file($fileTMP2, "{$path}/[TRANSCRIPT OF RECORDS] {$lname}, {$fname}.{$fileAExt2}");
        move_uploaded_file($fileTMP3, "{$path}/[HONORABLE DISMISSAL] {$lname}, {$fname}.{$fileAExt3}");
        move_uploaded_file($fileTMP4, "{$path}/[GOOD MORAL] {$lname}, {$fname}.{$fileAExt3}");
        insert($fname, $lname, $mname, $add, $email, $no, 0, $course, $gender, $year);
    }
?>

<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Colegio de Montalban Enrollment</title>
    <link rel="stylesheet" href="registerStyle.css">
    <script src="registerJs.js"></script>
</head>

<body>
    <div>
        <div class="tab">
            <button id="first" class="tablinks" onclick="openTab(event, 'freshmen')">Freshmen</button>
            <button class="tablinks" onclick="openTab(event, 'Y24')">2nd-4th Year</button>
            <button class="tablinks" onclick="openTab(event, 'transferee')">Transferee</button>
        </div>
        <div id="freshmen" class="container">
            <div class="title">Freshmen Registration</div>
            <form method="post" enctype="multipart/form-data">
                <div class="user-details">

                    <div class="input-box">
                        <span class="details">First Name</span>
                        <input type="text" name="Ffname" placeholder="Enter your first name" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Last Name</span>
                        <input type="text" name="Flname" placeholder="Enter your last name" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Middle Name</span>
                        <input type="text" name="Fmname" placeholder="Enter your middle name" required>
                    </div>

                    <div class="input-box">
                        <span class="details">E-Mail</span>
                        <input type="text" name="Femail" placeholder="Enter your email" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Number</span>
                        <input type="text" name="Fno" placeholder="Enter your number" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Address</span>
                        <input type="text" name="Fadd" placeholder="Enter your address" required>
                    </div>

                    <div class="course">
                        <center>
                            <label for="course">Choose course </label>
                            <select name="Fcourse" id="course" required>
                                <option hidden disabled selected value>-- select an option --</option>
                                <option value="BSIT">BSIT</option>
                                <option value="BSCPE">BSCPE</option>
                                <option value="BSED">BSED</option>
                            </select>
                        </center>
                    </div>

                </div>

                <div class="gender-details">
                    <input type="radio" name="Fgender" id="dot-1" value="Male">
                    <input type="radio" name="Fgender" id="dot-2" value="Female">
                    <span class="gender-title">Gender</span>
                    <div class="category">

                        <label for="dot-1">
                            <span class="dot one"></span>
                            <span class="gender">Male</span>
                        </label>

                        <label for="dot-2">
                            <span class="dot two"></span>
                            <span class="gender">Female</span>
                        </label>

                    </div>
                </div>

                <div class="files">
                    <center>
                        <input type="file" onchange="check(event)" name="FPfile" id="Ffile" required>
                        <label class="Ffile" for="Ffile">Upload 2x2 Picture</label>
                        <input type="file" onchange="check(event)" name="FBfile" id="Ffile1" required>
                        <label class="Ffile1" for="Ffile1">Upload Birth Certificate</label>
                        <input type="file" onchange="check(event)" name="FMfile" id="Ffile2" required>
                        <label class="Ffile2" for="Ffile2">Upload Moral Certificate</label>
                        <input type="file" onchange="check(event)" name="FFfile" id="Ffile3" required>
                        <label class="Ffile3" for="Ffile3">Upload Form 138</label>
                    </center>
                </div>

                <div class="button">
                    <input type="submit" value="Enroll" name="Fresh">
                </div>

            </form>
        </div>

        <div id="Y24" class="container">
            <div class="title">2nd-4th Year Registration</div>
            <form method="post" enctype="multipart/form-data">
                <div class="user-details">

                    <div class="input-box">
                        <span class="details">First Name</span>
                        <input type="text" name="Y24fname" placeholder="Enter your first name" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Last Name</span>
                        <input type="text" name="Y24lname" placeholder="Enter your last name" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Middle Name</span>
                        <input type="text" name="Y24mname" placeholder="Enter your middle name" required>
                    </div>

                    <div class="input-box">
                        <span class="details">E-Mail</span>
                        <input type="text" name="Y24email" placeholder="Enter your email" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Number</span>
                        <input type="text" name="Y24no" placeholder="Enter your number" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Address</span>
                        <input type="text" name="Y24add" placeholder="Enter your address" required>
                    </div>

                    <div class="sNum">
                        <center>
                            <span class="details">Student Number</span>
                            <input type="text" name="sNum" placeholder="Enter your student number" required>
                        </center>
                    </div>

                </div>
                

                <div class="files">
                    <center>
                        <input type="file" onchange="check(event)" name="Y24Ofile" id="Yfile" required>
                        <label class="Yfile" for="Yfile">Upload OVRF</label>
                        <input type="file" onchange="check(event)" name="Y24Sfile" id="Yfile1" required>
                        <label class="Yfile1" for="Yfile1">Upload SOG</label>
                    </center>
                </div>

                <div class="button">
                    <input type="submit" value="Enroll" name="Y24">
                </div>
            </form>
        </div>

        <div id="transferee" class="container">
            <div class="title">Transferee Registration</div>
            <form method="post" enctype="multipart/form-data">
                <div class="user-details">

                    <div class="input-box">
                        <span class="details">First Name</span>
                        <input type="text" name="Tfname" placeholder="Enter your first name" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Last Name</span>
                        <input type="text" name="Tlname" placeholder="Enter your last name" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Middle Name</span>
                        <input type="text" name="Tmname" placeholder="Enter your middle name" required>
                    </div>

                    <div class="input-box">
                        <span class="details">E-Mail</span>
                        <input type="text" name="Temail" placeholder="Enter your email" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Number</span>
                        <input type="text" name="Tno" placeholder="Enter your number" required>
                    </div>

                    <div class="input-box">
                        <span class="details">Address</span>
                        <input type="text" name="Tadd" placeholder="Enter your address" required>
                    </div>

                    <div class="year">
                        <center>
                        <label for="year">Select Year Level </label>
                            <select name="Tyear" id="year" required>
                                <option hidden disabled selected value>-- select an option --</option>
                                <option value="1">1st</option>
                                <option value="2">2nd</option>
                                <option value="3">3rd</option>
                                <option value="4">4th</option>
                            </select>
                        </center>
                    </div>

                    <div class="course">
                        <center>
                            <label for="course">Choose course </label>
                            <select name="Tcourse" id="course" required>
                                <option hidden disabled selected value>-- select an option --</option>
                                <option value="BSIT">BSIT</option>
                                <option value="BSCPE">BSCPE</option>
                                <option value="BSED">BSED</option>
                            </select>
                        </center>
                    </div>

                </div>

                <div class="gender-details">
                    <input type="radio" name="Tgender" id="dot3-1" value="Male">
                    <input type="radio" name="Tgender" id="dot3-2" value="Female">
                    <span class="gender-title">Gender</span>
                    <div class="category">

                        <label for="dot3-1">
                            <span class="dot one"></span>
                            <span class="gender">Male</span>
                        </label>

                        <label for="dot3-2">
                            <span class="dot two"></span>
                            <span class="gender">Female</span>
                        </label>

                    </div>
                </div>

                <div class="files">
                    <center>
                        <input type="file" onchange="check(event)" name="TPfile" id="Tfile" required>
                        <label class="Tfile" for="Tfile">Upload 2x2 Picture</label>
                        <input type="file" onchange="check(event)" name="TBfile" id="Tfile1" required>
                        <label class="Tfile1" for="Tfile1">Upload Birth Certificate</label>
                        <input type="file" onchange="check(event)" name="TTfile" id="Tfile2" required>
                        <label class="Tfile2" for="Tfile2">Upload Transcript of Records</label>
                        <input type="file" onchange="check(event)" name="THfile" id="Tfile3" required>
                        <label class="Tfile3" for="Tfile3">Upload Honorable Dismissal</label>
                        <input type="file" onchange="check(event)" name="TGfile" id="Tfile4" required>
                        <label class="Tfile4" for="Tfile4">Upload Good Moral Certificate</label>
                    </center>
                </div>

                <div class="button">
                    <input type="submit" value="Enroll" name="Trans">
                </div>
            </form>
        </div>
    </div>
</body>

</html>