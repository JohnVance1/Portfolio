window.addEventListener('mousemove', nutterbutter);
function nutterbutter(){
	var _el = document.getElementById('gameProject');
	_el.style.top = event.clientY + "px";
	_el.style.left = event.clientX + "px";
}

// Additionally, there is no requirement to have the initial event	listener be attached to window..


// var _el = document.getElementsById('gameProjects');
// _el.addEventListener('dblclick', function(){
// 	window.addEventListener('mousemove', nutterbutter);
// 	_el.addEventListener('dblclick', removeNutter);
// });

// function removeNutter(){
// 	window.removeEventListener('mousemove', nutterbutter);
// 	_el.removeEventListener('dblclick', removeNutter);
// }
// function nutterbutter(){
// 	_el.style.top = event.clientY + "px";
// 	_el.style.left = event.clientX + "px";
// }