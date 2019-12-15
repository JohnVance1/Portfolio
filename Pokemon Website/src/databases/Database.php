<!-- PHP

// $dex = (object) [
//     '1' => [
//         'name' => "Bulbasaur",
//         'type' => "Grass/Poison",
//         'sprite' => '../../pokemon/icons/gen7/001.png'
//     ]


// ];

?> -->

<?php
    $servername = "localhost";
    $username = "username";
    $password = "password";
    $dbname = "myDB";

    // Create connection
    $conn = mysqli_connect($servername, $username, $password, $dbname);
    // Check connection
    if (!$conn) {
        die("Connection failed: " . mysqli_connect_error());
    }

    // sql to create table
    // $sql = "CREATE TABLE MyGuests (
    // id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY, 
    // firstname VARCHAR(30) NOT NULL,
    // lastname VARCHAR(30) NOT NULL,
    // email VARCHAR(50),
    // reg_date TIMESTAMP
    // )";

    $sql = "INSERT INTO MyGuests (firstname, lastname, email)
    VALUES ('John', 'Doe', 'john@example.com')";

    if ($conn->query($sql) === TRUE) {
        $last_id = $conn->insert_id;
        echo "New record created successfully. Last inserted ID is: " . $last_id;
    } else {
        echo "Error: " . $sql . "<br>" . $conn->error;
    }

    mysqli_close($conn);
?>