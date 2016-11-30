!function() {
    'use strict';

    var customerService = window.customerService || {};
    var list = document.querySelector('#list');

    customerService.itemAdded = function() {
        var items = JSON.parse(customerService.list());

        while (list.firstChild) {
            list.removeChild(list.firstChild);
        }

        items.forEach(function(item) {
            var childNode = document.createElement('li');
            childNode.innerHTML = item.Id + ' ' + item.FirstName + ' ' + item.LastName;
            childNode.onclick = function() {
                customerService.selectItem(item.Id);
            };

            list.appendChild(childNode);
        });
    };
}();