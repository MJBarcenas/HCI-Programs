<style>
    @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@200;300;400;500;600;700&display=swap');
    p {
        font-family: 'Poppins';
    }
</style>

<?php
$allowed = array('jpg', 'jpeg', 'png', 'pdf');
if (isset($_POST['Fresh'])) {
    $fname = $_POST['Ffname'];
    $lname = $_POST['Flname'];
    $mname = $_POST['Fmname'];
    $email = $_POST['Femail'];
    $no = $_POST['Fno'];
    $add = $_POST['Fadd'];
    $gender = $_POST['Fgender'];

    $file = $_FILES['Pfile'];

    $fileName = $_FILES['Pfile']['name'];
    $fileType = $_FILES['Pfile']['type'];
    $fileSize = $_FILES['Pfile']['size'];
    $fileError = $_FILES['Pfile']['error'];
    $fileTMP = $_FILES['Pfile']['tmp_name'];

    $fileExt = explode('.', $fileName);
    $fileAExt = strtolower(end($fileExt));

    $file1 = $_FILES['Bfile'];

    $fileName1 = $_FILES['Bfile']['name'];
    $fileType1 = $_FILES['Bfile']['type'];
    $fileSize1 = $_FILES['Bfile']['size'];
    $fileError1 = $_FILES['Bfile']['error'];
    $fileTMP1 = $_FILES['Bfile']['tmp_name'];

    $fileExt1 = explode('.', $fileName1);
    $fileAExt1 = strtolower(end($fileExt1));

    if (in_array($fileAExt, $allowed) and in_array($fileAExt1, $allowed)) {
        if ($fileError === 0 and $fileError1 === 0) {
            if ($fileSize < 100000000 and $fileSize1 < 1000000000) {
                move_uploaded_file($fileTMP, "uploads/".$lname.", ".$fname.".".$fileAExt);
                move_uploaded_file($fileTMP1, "uploads/".$lname.", ".$fname.".".$fileAExt1);
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
    echo "clicked 2";
} elseif (isset($_POST['Y24'])) {
    echo "clicked 3";
} else {
    echo "tanginamo";
}

echo "Gumana";