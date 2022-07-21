<?php
require "DataBase.php";
$db = new DataBase();
if (isset($_POST['studentnum']) && isset($_POST['accpass'])) {
    if ($db->dbConnect()) {
        if ($db->logIn("students", $_POST['studentnum'], $_POST['accpass'])) {
            #echo $db->logIn("students", $_POST['studentnum'], $_POST['accpass']);

            $conn = new mysqli("localhost", "root", "", "onlineenrollment");

            $sql = "SELECT studentnum, gender, firstname, lastname, middlename, course, section FROM students WHERE studentnum='{$_POST['studentnum']}'";
            $result = $conn->query($sql);
            $result = $result->fetch_array(MYSQLI_NUM);
            array_push($result, "Login Success");
            #$list = join(",", $result);
            $conn->close();

            echo join(",", $result);
        } else echo "Username or Password wrong";
    } else echo "Error: Database connection";
} else echo "All fields are required";
?>
