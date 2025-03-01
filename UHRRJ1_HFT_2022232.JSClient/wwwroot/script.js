﻿let authors = [];
let connection = null;
let i = 1;
getdata();
setupSignalR();

let authorIdToUpdate = -1;


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:23125/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("AuthorCreated", (user, message) => {
        getdata();
    });

    connection.on("AuthorDeleted", (user, message) => {
        getdata();
    });

    connection.on("AuthorUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:23125/author')
        .then(x => x.json())
        .then(y => {
            authors = y;
            console.log(y);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    authors.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr>" +
                "<td>" + t.authorId + "</td>" +
                "<td>" + t.authorName + "</td>" +
                "<td>" + `<button type="button" onclick="remove(${t.authorId})">Delete</button>` +
                `<button type="button" onclick="showupdate(${t.authorId})">Update</button>` +
                `<button type="button" onclick="books('${encodeURIComponent(JSON.stringify(t.authorName))}')">Books</button>` +
                `<button type="button" onclick="stores('${encodeURIComponent(JSON.stringify(t.authorName))}')">Stores</button>` +
                "</td>" +
            "</tr>";
    });
}

function showupdate(id) {
    document.getElementById('authornametoupdate').value = authors.find(t => t['authorId'] == id)['authorname'];
    document.getElementById('updateformdiv').style.display = 'block';
    authorIdToUpdate = id;
}

function remove(id) {
    fetch('http://localhost:23125/author/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('authornametoupdate').value;
    fetch('http://localhost:23125/author', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { authorName: name, authorId: authorIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('authorname').value;
    fetch('http://localhost:23125/author', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { authorName: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function orderedlist() {
    document.getElementById('noncrud_area').innerHTML = "<tr><td>Name</td><td>Books</td></tr>";
    fetch('http://localhost:23125/AuthorBookNumber/AuthorsByNumberOfBooks/')
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            data.forEach(t => {
                document.getElementById('noncrud_area').innerHTML += "<tr><td>" + t.name + "</td><td>" + t.bookCount + "</td></tr>";
                i++;
            })
        })
        .catch((error) => { console.error('Error:', error); });
}

function books(arg) {
    document.getElementById('noncrud_area').innerHTML = "";
    name = JSON.parse(decodeURIComponent(arg));
    url = 'http://localhost:23125/WrittenBooks/WrittenBooks?' + new URLSearchParams({ authorName: name }).toString();
    fetch(url)
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            data.forEach(book => {
                document.getElementById('noncrud_area').innerHTML += "<tr><td>" + book.title + "</td></tr>";
                i++;
            })
        })
        .catch((error) => { console.error('Error:', error); });

    console.log("Fetched from: ");
    console.log(url);
}

function stores(arg) {
    document.getElementById('noncrud_area').innerHTML = "";
    name = JSON.parse(decodeURIComponent(arg));
    url = 'http://localhost:23125/AuthorsStores/Stores?' + new URLSearchParams({ authorName: name }).toString();
    fetch(url)
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            data.forEach(t => {
                document.getElementById('noncrud_area').innerHTML += "<tr><td>" + t.bookStoreName + "</td></tr>";
                i++;
            })
        })
        .catch((error) => { console.error('Error:', error); });

    console.log("Fetched from: ");
    console.log(url);
}
