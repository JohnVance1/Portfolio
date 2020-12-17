<?php   
 	// ** Form validation code **
 	// We will use the $_POST "super global" associative array to extract the values of the form fields
	// #1 - was the submit button pressed?
    if (isset($_POST["submit"])){ 
    	$to = "jmv7411@rit.edu"; // !!! REPLACE WITH YOUR EMAIL !!!
    	
    	// #2 - if a value for the `email` form field is missing, give a default value
    	// else, use the value from the form field
	$from = empty(trim($_POST["email"])) ? "noemail@sample.com" : sanitize_string($_POST["email"]);
			
	$subject = "Web Form";
			
	// #3 - same as above, except with the `message` form field
	$message = empty(trim($_POST["message"])) ?  "No message" : sanitize_string($_POST["message"]);
			
	// #4 - same as above, except with the `name` form field
	$name = empty(trim($_POST["name"])) ? "No name" : sanitize_string($_POST["name"]);
			
	// #5 - same as above, except with the `human` form field
	$human = empty(trim($_POST["human"])) ? "0" : sanitize_string($_POST["human"]);
			
	$headers = "From: $from" . "\r\n";
			
	// #6 - add the user's name to the end of the message
	$message .= "\n\n - $name";
				
	
    // go back to form page when we are done
	header("Location: form.html"); // #10 - CHANGE THIS TO THE NAME OF YOUR FORM PAGE - AN ABSOLUTE URL WOULD BE EVEN BETTER
	exit();
	}
	
    // #9 - this handy helper function is very necessary whenever
    // we are going to put user input onto a web page or a database
    // For example, if the user entered a <script> tag, and we added that <script> tag to our HTML page
    // they could perform an XSS attack (Cross-site scripting)
    function sanitize_string($string){
	$string = trim($string);
	$string = strip_tags($string);
	return $string;
    }
?>