<?php
require "DataBase.php";
$db = new DataBase();
if (isset($_POST['studentnum']) && isset($_POST['accpass'])) {
    if ($db->dbConnect()) {
        if ($db->logIn("students", $_POST['studentnum'], $_POST['accpass'])) {
            #echo $db->logIn("students", $_POST['studentnum'], $_POST['accpass']);
            echo "Login Success";
        } else echo "Username or Password wrong";
    } else echo "Error: Database connection";
} else echo "All fields are required";
?>
