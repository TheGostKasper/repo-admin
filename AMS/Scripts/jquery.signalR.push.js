function SignalREntry() {
    $.connection.hub.url = (isWebsiteLive) ? 'https://www.weelo.com.eg/api/signalr' : '/api/signalr';
    var notificationHub = $.connection.notificationHub;

    function hubStarted() {
        return  $.connection.hub.start()
    };
    function getConnectionHub() {
        return $.connection.notificationHub;
    }
    function getConnectionId() {
        return $.connection.hub.id;
    }
    this.getConnectionId = getConnectionId
    this.getConnectionHub = getConnectionHub;
    this.getHubStarted = hubStarted;
};
