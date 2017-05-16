var AmoCrm = function () {
	if (!window.reqwest) {
		console.log("No reqwest");
		return null;
	}

	function handleAuth(login, hash) {
		//$.ajax({
		//	url: "https://shinecity.amocrm.ru/private/api/auth.php?type=json",
		//    data: {
		//        'USER_LOGIN': login,
		//        'USER_HASH': hash
		//    },
		//    contentType: "application/json; charset=utf-8",
		//	dataType: "json",
		//    method: "post",
		//    xhrFields: {
		//        withCredentials: true
		//    },
		//    headers: {
		//        "Access-Control-Allow-Origin": "*",
		//        "Allow" : "OPTIONS, GET, HEAD, POST"
		//    },

		//    crossDomain: true,

		//    error: function(err) {
		//        console.log("Error: " + err);
		//    },
		//    success: function(resp) {
		//        console.log("Success: " + resp);
		//    }
		//});
		window.reqwest({
			url: "https://shinecity.amocrm.ru/private/api/auth.php?type=json",
			data: {
				'USER_LOGIN': login,
				'USER_HASH': hash
			},
			//contentType: "application/json; charset=utf-8",
			method: "post",
			type: "json",
			crossOrigin: true,
			withCredentials: true,
			error: function (err) {
				console.log("Error: " + err);
			},
			success: function (resp) {
				console.log("Success: " + resp);
			}
		});
	};

	function handleUpdateLead() {

	};

	return {
		auth: function (login, hash) {
			handleAuth(login, hash);
		},

		updateLead: function () {
			handleUpdateLead();
		}
	}
}();