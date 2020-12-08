
angular.module("umbraco").controller("CustomWelcomeDashboardController", function ($scope) {

	function setWithExpiry(key, value, ttl) {
		const now = new Date()

		// `item` is an object which contains the original value
		// as well as the time when it's supposed to expire
		const item = {
			value: value,
			expiry: now.getTime() + ttl,
		}
		localStorage.setItem(key, JSON.stringify(item))
	}

	function getWithExpiry(key) {
		const itemStr = localStorage.getItem(key)

		// if the item doesn't exist, return null
		if (!itemStr) {
			return null
		}

		const item = JSON.parse(itemStr)
		const now = new Date()

		// compare the expiry time of the item with the current time
		if (now.getTime() > item.expiry) {
			// If the item is expired, delete the item from storage
			// and return null
			localStorage.removeItem(key)
			return null
		}
		return item.value
	}


	const QUOTE_CACHE = 'quoteCache'
	var storedQuote = getWithExpiry(QUOTE_CACHE)

    var vm = this;
	vm.hasData = false;
	vm.isLoading = true;
    vm.quoteData;

    if (!storedQuote) {
        fetch("https://quotes.rest/qod?language=en")
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                vm.quoteData = data;
                vm.hasData = true;
				setWithExpiry(QUOTE_CACHE, JSON.stringify(data), 1000*60*60*12) // 1sec * 60 = 1min => 1min*60 = 1h => 1h*12 = more than a workday
				vm.isLoading = false
            });
    } else {
        vm.quoteData = JSON.parse(storedQuote)
        vm.hasData = true;
		vm.isLoading = false
	}

	

});

