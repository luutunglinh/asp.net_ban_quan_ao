var Contact = {
    onComplete: function (content) {
        var result = eval(content.get_response().get_object());

        var textNode = document.createTextNode(result.message);

        document.getElementById("result").appendChild(textNode);
    }
};