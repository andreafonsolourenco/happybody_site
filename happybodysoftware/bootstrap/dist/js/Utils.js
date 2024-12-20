 

function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

 
function numericSorter(a, b) {
    if (isNumeric(a))
    {
        if (parseFloat(a) > parseFloat(b)) return 1;
        if (parseFloat(a) < parseFloat(b)) return -1;
        return 0;
    }
    else {
        if (a > b) return 1;
        if (a < b) return -1;
        return 0;
    }
}
