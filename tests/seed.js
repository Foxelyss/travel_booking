"use strict";

const server = "http://localhost:5165";

var requests = [];

let points = [
    { city: 'г. Казань', region: 'Казанская область', name: 'Казань' },
    { city: 'г. Севастополь', region: 'Севастополь', name: 'Севастополь' }
];


for (let point of points) {
    const response = fetch(server + "/api/point/", {
        "credentials": "omit",
        "headers": {
            "User-Agent": "Mozilla/5.0 (X11; Linux x86_64; rv:145.0) Gecko/20100101 Firefox/145.0",
            "Accept": "*/*",
            "Accept-Language": "ru,en-US;q=0.7,en;q=0.3",
            "Sec-Fetch-Dest": "empty",
            "Sec-Fetch-Mode": "no-cors",
            "Sec-Fetch-Site": "same-origin",
            "Priority": "u=4",
            "Pragma": "no-cache",
            "Cache-Control": "no-cache",
            "Content-Type": "application/json"
        },
        "body": JSON.stringify(point),
        "method": "POST",
        "mode": "cors"
    }).then(function (response) {
        return response.json();
    });

    requests.push(response);
}

let companies = [
    { name: 'ООО Лана', INN: '123123', address: 'г. Томск', phone: '8 963 312 75 66' },
    { name: 'ООО РЖД', INN: '12312389', address: 'г. Москва', phone: '8 123 312 75 66' },
];

for (let company of companies) {
    const response = fetch(server + "/api/company", {
        "credentials": "omit",
        "headers": {
            "User-Agent": "Mozilla/5.0 (X11; Linux x86_64; rv:145.0) Gecko/20100101 Firefox/145.0",
            "Accept": "*/*",
            "Accept-Language": "ru,en-US;q=0.7,en;q=0.3",
            "Sec-Fetch-Dest": "empty",
            "Sec-Fetch-Mode": "no-cors",
            "Sec-Fetch-Site": "same-origin",
            "Priority": "u=4",
            "Pragma": "no-cache",
            "Cache-Control": "no-cache",
            "Content-Type": "application/json"
        },
        "body": JSON.stringify(company),
        "method": "POST",
        "mode": "cors"
    }).then(function (response) {
        return response.json();
    });

    requests.push(response);
}


let means = [
    { name: 'Ж/Д' },
    { name: 'Автобусом' },
];

for (let mean of means) {
    const response = fetch(server + "/api/mean", {
        "credentials": "omit",
        "headers": {
            "User-Agent": "Mozilla/5.0 (X11; Linux x86_64; rv:145.0) Gecko/20100101 Firefox/145.0",
            "Accept": "*/*",
            "Accept-Language": "ru,en-US;q=0.7,en;q=0.3",
            "Sec-Fetch-Dest": "empty",
            "Sec-Fetch-Mode": "no-cors",
            "Sec-Fetch-Site": "same-origin",
            "Priority": "u=4",
            "Pragma": "no-cache",
            "Cache-Control": "no-cache",
            "Content-Type": "application/json"
        },
        "body": JSON.stringify(mean),
        "method": "POST",
        "mode": "cors"
    }).then(function (response) {
        return response.json();
    });

    requests.push(response);
}


const transports = [{
    "name": "name",
    "departurePoint": 1,
    "arrivalPoint": 2,
    "departure": (new Date(2026, 2, 10, 2, 30)).toISOString(),
    "arrival": (new Date(2026, 2, 11, 2, 30)).toISOString(),
    "transportingMean": 1,
    "company": 1,
    "price": 100,
    "placeCount": 120,
    "freePlaceCount": 100
}, {
    "name": "asdname",
    "departurePoint": 2,
    "arrivalPoint": 1,
    "departure": (new Date(2026, 2, 10, 2, 30)).toISOString(),
    "arrival": (new Date(2026, 2, 11, 2, 30)).toISOString(),
    "transportingMean": 2,
    "company": 2,
    "price": 32,
    "placeCount": 40,
    "freePlaceCount": 10
}];


for (let transport of transports) {
    const response = fetch(server + "/api/transport", {
        "credentials": "omit",
        "headers": {
            "User-Agent": "Mozilla/5.0 (X11; Linux x86_64; rv:145.0) Gecko/20100101 Firefox/145.0",
            "Accept": "*/*",
            "Accept-Language": "ru,en-US;q=0.7,en;q=0.3",
            "Sec-Fetch-Dest": "empty",
            "Sec-Fetch-Mode": "no-cors",
            "Sec-Fetch-Site": "same-origin",
            "Priority": "u=4",
            "Pragma": "no-cache",
            "Cache-Control": "no-cache",
            "Content-Type": "application/json"
        },
        "body": JSON.stringify(transport),
        "method": "POST",
        "mode": "cors"
    }).then();

    requests.push(response);
}


Promise.all(requests).then((results) => {
}).catch(function (err) {
    console.log(err);
});
