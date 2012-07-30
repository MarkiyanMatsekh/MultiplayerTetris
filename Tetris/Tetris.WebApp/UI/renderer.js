(function () {
    var wsServer = 'ws://localhost:8080/websession';
    var socket = null; // we'll keep reference here
    var gameMatrix = null;

    var gameFieldContainer = $('.game-field-container');
    var startBtn = $('.start-game').click(function () {
        gameFieldContainer.text('starting game...');
        toggleButtons();
        createConnection();
    });

    var stopBtn = $('.stop-game').click(function () {
        gameFieldContainer.text('wanna start a new one?');
        toggleButtons();
        socket.disconnect();
    });

    function createConnection() {
        var actions = {
            init: init,
            render: render
        };

        socket = new WebSocket(wsServer);
        socket.onopen = function () {
            log('connected To Server');
        };
        socket.onmessage = function (event) {
            var data = $.parseJSON(event.data);
            var msgType = data.msgType;
            actions[msgType](data);
        };
        socket.onclose = function () {
            log("connection with server closed");
            socket = null;
        };
        socket.onerror = function (err) {
            log(err);
        };
    };

    function toggleButtons() {
        startBtn.toggle();
        stopBtn.toggle();
    }

    function init(data) {
        var size = data.size;
        gameFieldContainer.text("");
        gameMatrix = [];

        var i, j, width, height;
        for (i = 0, width = size.width; i < width; i++) {
            gameMatrix[i] = [];
        }

        var gameField = $('<table class="game-field"/>');
        for (i = 0, height = size.height; i < height; i++) {
            var row = $('<tr/>');
            for (j = 0, width = size.width; j < width; j++) {
                var cell = $("<td class='game-field-cell'/>");
                gameMatrix[j][i] = cell;
                row.append(cell);
            }
            gameField.append(row);
        }

        gameFieldContainer.append(gameField);
    }

    function render(data) {
        var coords = data.coords;
        for (var i = 0, l = coords.length; i < l; i++) {
            var coord = coords[i];
            var color = coord.color.toLowerCase();
            var cell = gameMatrix[coord.x][coord.y];
            cell.css('background-color', color);
        }
    }

    var output = $('.output');
    function log(text) {
        output.append("<div>" + text + "</div>");
    }

})();