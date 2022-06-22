<?php
    $fname = "Michael Justin";
    $lname = "Barcenas";
    $email = "imaqtchael@gmail.com";

    $conn = new mysqli("localhost", "root", "", "onlineenrollment");

    $sql = "SELECT studentnum FROM students WHERE (firstname='{$fname}' AND lastname='{$lname}') AND email='{$email}'";
    $result = $conn->query($sql);
    $result = $result->fetch_array(MYSQLI_NUM);

    echo $result[0];

    $sql = "SELECT * FROM students WHERE (firstname='{$fname}' AND lastname='{$lname}') AND email='{$email}'";
    $result = $conn->query($sql);
    $result = $result->fetch_array(MYSQLI_NUM);
    $conn->close();

    try {
        if (count($result) > 1) {
        echo "present";
        }
    } catch (\Throwable $th) {
        echo "not present";
    }

    

?>