<?php
if (isset($_POST['submit'])) {
    $file = $_FILES['file'];

    $fileName = $_FILES['file']['name'];
    $fileType = $_FILES['file']['type'];
    $fileSize = $_FILES['file']['size'];
    $fileError = $_FILES['file']['error'];
    $fileTMP = $_FILES['file']['tmp_name'];

    $fileExt = explode('.', $fileName);
    $fileAExt = strtolower(end($fileExt));

    $allowed = array('jpg', 'jpeg', 'png', 'pdf');

    if (in_array($fileAExt, $allowed)) {
        if ($fileError === 0) {
            if ($fileSize < 100000000) {
                move_uploaded_file($fileTMP, "uploads/here.".$fileAExt);
                echo "SUCCESS!";
            } else {
                echo "The file is too big!";
            }
        } else {
            echo "There was an error uploading your file";
        }
    } else {
        echo "You are not allowed to upload this file type!";
    }

    echo "WEWS";
}

$rand = $_POST['submitted'];

print_r($rand);