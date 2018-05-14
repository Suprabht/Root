/* 1. inconsistency in ;
var foo = function (n) {  

    console.log("Hello World from MAIN!!");

    if (n > 5)
    {
        return
            n * 2
    }
    else
    {
        return n;
    } 
};
console.log(foo(6));*/

/*2. Defining a function
function foo()
{
    console.log("Defined...")
}

foo();
function foo()
{
    console.log("Redifine....")
}

foo();*/

/*3. argument of function 
var max = function (a, b)
{
    if (a > b)
        return a
    else
        return b;
}
console.log(max(2, 3, 4));
max = function ()
{
    var large = arguments[0];
    for (var i = 0; i < arguments.length; i++)
    {
        if (large < arguments[i])
            large = arguments[i];
    }
    return large;
}
console.log(max(2,3,9,12));
*/

/*4. Override console.log
console.log = function (message)
{
    document.getElementById("demo").innerHTML = message;
}
console.log("I am a good boy!")*/

/*5. apply and call

function greet(name)
{
    console.log(this.toUpperCase() + " " + name);
}

greet.call("hi", "Suprabhat");
greet.call("Namestha", "Arun");
greet.apply("hello", ["Arun"]);*/

/*6. Higher order junction
var list = [1, 2, 3, 4, 5, 6, 7, 8];
list.filter(function (e) { return e % 2 == 0 })
    .map(function (e) { return e * 2 })
    .forEach(function (e) { console.log(e) });
*/

/*7. a=b b=c, c!=a
    var a = "1";
    var b = 1;
    var c = "1.0";
    console.log(a==b);
    console.log(b==c);
    console.log(c==a);*/

var receiveMessageFromChild = function (event) {
    // Do we trust the sender of this message?  (might be
    // different from what we originally opened, for example).
    // if (event.origin !== "http://example.com")
    //     return;

    // event.source is popup
    // event.data is "hi there yourself!  the secret response is: rheeeeet!"
}
window.addEventListener("message", receiveMessageFromChild, false);