
function multipleBoxShadow(n) {
    let value = '';
    for (let i = 0; i < n; i++) {
        value += `${Math.random() * 2000}px ${Math.random() * 2000}px #FFF, `
    }
    return value.slice(0, -2); // remove last comma and space
}

document.getElementById('stars').style.boxShadow = multipleBoxShadow(700);
document.getElementById('stars2').style.boxShadow = multipleBoxShadow(200);
document.getElementById('stars3').style.boxShadow = multipleBoxShadow(100);

document.getElementById('stars').style.width = '1px';
document.getElementById('stars').style.height = '1px';

document.getElementById('stars2').style.width = '2px';
document.getElementById('stars2').style.height = '2px';

document.getElementById('stars3').style.width = '3px';
document.getElementById('stars3').style.height = '3px';

