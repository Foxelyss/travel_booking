"use strict";

// const server = "https://travel.foxelyss.ru";
const server = "http://localhost:5165";

var requests = [];

let points = [
    { city: 'г. Томск', region: 'Томская область', name: 'Томск' },
    { city: 'г. Новосибирск', region: 'Новосибирская область', name: 'Новосибирск' },
    { city: 'г. Казань', region: 'Казанская область', name: 'Казань' },
    { city: 'г. Магадан', region: 'Магаданская область', name: 'Новосибирск' },
    { city: 'г. Самара', region: 'Самарская область', name: 'Самара' },
    { city: 'г. Москва', region: 'Москва', name: 'Москва' },
    { city: 'г. Санкт-Петербург', region: 'Санкт-Петербург', name: 'Санкт-Петербург' },
    { city: 'г. Горно-Алтайск', region: 'Республика Алтай', name: 'Горно-Алтайск' },
    { city: 'г. Нижний Новгород', region: 'Нижегородская область', name: 'Нижний Новгород' },
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
    { name: 'ИП Сидоров', INN: '5645645623', address: 'г. Томск', phone: '8 963 312 75 66' },
    { name: 'ИП Петров', INN: '121233123', address: 'г. Новгород', phone: '8 963 312 75 66' },
    { name: 'ИП Добраков', INN: '1238678123', address: 'г. Томск', phone: '8 963 312 75 66' },
    { name: 'ИП Сергеев', INN: '123126783', address: 'г. Новосибирск', phone: '8 963 122 86 66' }
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
    { name: 'Самолётом' },
    { name: 'Трансфер' },
    { name: 'Электричкой' }
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


function randrange(high, low) {
    return Math.floor(Math.random() * (high - low) + low);
}

for (let i = 0; i < 500; i++) {
    let city = randrange(1, 11);

    let end_city = city;

    while (city == end_city) {
        end_city = randrange(1, 11)
    }

    let company = randrange(1, 7);
    let means = [];
    let means_count = randrange(1, 3)
    for (let i = 0; i < means_count; i++) {
        means.push(randrange(1, 5));
    }
    let name = String.fromCharCode(randrange("А".charCodeAt(0), "Я".charCodeAt(0) + 1)) + "-" + String(randrange(1, 315));
    let places = randrange(30, 400);

    let random_date_in_future = Date.now() + randrange(86400, 5184000) * 1000;
    let random_date_in_future_next = random_date_in_future + randrange(5200, 57600) * 1000;

    let price = randrange(10000, 6500000) / 100;

    let date_start = new Date();
    date_start.setTime(random_date_in_future);

    let date_end = new Date();
    date_end.setTime(random_date_in_future_next);

    const transport = {
        "name": name,
        "departurePoint": city,
        "arrivalPoint": end_city,
        "departure": date_start.toISOString(),
        "arrival": date_end.toISOString(),
        "transportingMean": means,
        "company": company,
        "price": price,
        "placeCount": places,
        "freePlaceCount": places
    };

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
    // console.log(JSON.stringify(results, null, 2));
}).catch(function (err) {
    console.log(err);
});
