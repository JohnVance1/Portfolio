
window.onload = (function() {
	SetHeight();

});



window.onresize = (function() {
	SetHeight();

});


function SetHeight(){
	var navHeight = document.getElementById("navbar").offsetHeight;
	var heightGoal = navHeight;
	document.getElementById("header").style.height = heightGoal + "px";


}

// function myFunction() {
// 	document.getElementById("dropdownPersonal").classList.toggle("show");
//   }
  
//   // Close the dropdown if the user clicks outside of it
//   window.onclick = function(e) {
// 	if (!e.target.matches('.drop-dwn')) {
// 	var myDropdown = document.getElementById("dropdownPersonal");
// 	  if (myDropdown.classList.contains('show')) {
// 		myDropdown.classList.remove('show');
// 	  }
// 	}
//   }
