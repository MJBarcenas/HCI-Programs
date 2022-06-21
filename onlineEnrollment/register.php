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
    
        if (in_array($fileAExt, array('jpg', 'jpeg', 'png')) and $fileAExt1 == 'pdf') {
            if ($fileError === 0 and $fileError1 === 0) {
                if ($fileSize < 100000000 and $fileSize1 < 1000000000) {
                    mkdir("uploads/Freshmen/".$lname.", ".$fname);
                    move_uploaded_file($fileTMP, "uploads/Freshmen/".$lname.", ".$fname."/".$lname.", ".$fname.".".$fileAExt);
                    move_uploaded_file($fileTMP1, "uploads/Freshmen/".$lname.", ".$fname."/".$lname.", ".$fname.".".$fileAExt1);
                    
                    insert($fname, $lname, $mname, $add, $email, $no, 0);
                } else {
                    echo "<p>One or both files are too big!</p>";
                }
            } else {
                echo "There was an error uploading your files!";
            }
        } else {
            echo "<script>alert('You are not allowed to upload this file type!')</script>";
        }
    
    } elseif (isset($_POST['Y24'])) {
        $fname = $_POST['Y24fname'];
        $lname = $_POST['Y24lname'];
        $mname = $_POST['Y24mname'];
        $email = $_POST['Y24email'];
        $no = $_POST['Y24no'];
        $add = $_POST['Y24add'];
        $gender = $_POST['Y24gender'];

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

        if ($fileAExt == "pdf" and $fileAExt1 == "pdf") {
            if ($fileError === 0 and $fileError1 === 0) {
                if ($fileSize < 100000000 and $fileSize1 < 1000000000) {
                    mkdir("uploads/2nd-4th Year/".$lname.", ".$fname);
                    move_uploaded_file($fileTMP, "uploads/2nd-4th Year/".$lname.", ".$fname."/[OVRF] ".$lname.", ".$fname.".".$fileAExt);
                    move_uploaded_file($fileTMP1, "uploads/2nd-4th Year/".$lname.", ".$fname."/[SSOG] ".$lname.", ".$fname.".".$fileAExt1);
                    echo "<script>alert('UPLOAD SUCCESS!')</script>";
                } else {
                    echo "<p>One or both files are too big!</p>";
                }
            } else {
                echo "There was an error uploading your files!";
            }
        } else {
            echo "<script>alert('You are not allowed to upload this file type!')</script>";
        }
    } elseif (isset($_POST['Trans'])) {
        $fname = $_POST['Tfname'];
        $lname = $_POST['Tlname'];
        $mname = $_POST['Tmname'];
        $email = $_POST['Temail'];
        $no = $_POST['Tno'];
        $add = $_POST['Tadd'];
        $gender = $_POST['Tgender'];

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

        if ($fileAExt == "pdf" and $fileAExt1 == "pdf") {
            if ($fileError === 0 and $fileError1 === 0) {
                if ($fileSize < 100000000 and $fileSize1 < 1000000000) {
                    mkdir("uploads/2nd-4th Year/".$lname.", ".$fname);
                    move_uploaded_file($fileTMP, "uploads/2nd-4th Year/".$lname.", ".$fname."/[OVRF] ".$lname.", ".$fname.".".$fileAExt);
                    move_uploaded_file($fileTMP1, "uploads/2nd-4th Year/".$lname.", ".$fname."/[SSOG] ".$lname.", ".$fname.".".$fileAExt1);
                    echo "<script>alert('UPLOAD SUCCESS!')</script>";
                } else {
                    echo "<p>One or both files are too big!</p>";
                }
            } else {
                echo "There was an error uploading your files!";
            }
        } else {
            echo "<script>alert('You are not allowed to upload this file type!')</script>";
        }
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

                </div>

                <div class="gender-details">
                    <input type="radio" name="Fgender" id="dot-1" value="male">
                    <input type="radio" name="Fgender" id="dot-2" value="female">
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
                        <input type="file" name="FPfile" id="Ffile">
                        <label for="Ffile">Upload 1x1 Picture</label>
                        <input type="file" name="FBfile" id="Ffile1">
                        <label for="Ffile1">Upload Birth Certificate</label>
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

                </div>

                <div class="gender-details">
                    <input type="radio" name="Y24gender" id="dot2-1">
                    <input type="radio" name="Y24gender" id="dot2-2">
                    <span class="gender-title">Gender</span>
                    <div class="category">

                        <label for="dot2-1">
                            <span class="dot one"></span>
                            <span class="gender">Male</span>
                        </label>

                        <label for="dot2-2">
                            <span class="dot two"></span>
                            <span class="gender">Female</span>
                        </label>

                    </div>
                </div>

                <div class="files">
                    <center>
                        <input type="file" name="Y24Ofile" id="file">
                        <label for="file">Upload OVRF</label>
                        <input type="file" name="Y24Sfile" id="file1">
                        <label for="file1">Upload SOG</label>
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

                </div>

                <div class="gender-details">
                    <input type="radio" name="Tgender" id="dot3-1">
                    <input type="radio" name="Tgender" id="dot3-2">
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
                        <input type="file" name="file" id="Tfile">
                        <label for="Tfile">Upload 1x1 Picture</label>
                        <input type="file" name="file" id="Tfile1">
                        <label for="Tfile1">Upload Birth Certificate</label>
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