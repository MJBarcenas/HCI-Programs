<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <form>
    <input type="text" name="num1">
    <input type="text" name="num2">
    <select name="operator">
        <option>ADD</option>
        <option>SUBTRACT</option>
        <option>MULTIPLY</option>
        <option>DIVIDE</option>
    </select>
    <button type="submit" name="submit">GO!</button>

    <p>The answer is: </p>
    </form>

    <?php

        function add($num1, $num2) {
            echo $num1 + $num2;
        }

        function sub($num1, $num2) {
            echo $num1 - $num2;
        }

        function multi($num1, $num2) {
            echo $num1 * $num2;
        }

        function div($num1, $num2) {
            echo $num1 / $num2;
        }
        $lname = "Barcenas";
        $fname = "Michael Justin";
        if (isset($_GET['submit'])) {
            if ($_GET['operator'] == 'ADD') {
                add($_GET['num1'], $_GET['num2']);
                if (mkdir($lname.", ".$fname)){
                    echo "here";
                }
            } elseif ($_GET['operator'] == 'SUBTRACT') {
                sub($_GET['num1'], $_GET['num2']);
            } elseif ($_GET['operator'] == 'MULTIPLY') {
                multi($_GET['num1'], $_GET['num2']);
            } elseif ($_GET['operator'] == 'DIVIDE') {
                div($_GET['num1'], $_GET['num2']);
            }
        }
    ?>
    
</body>
</html>