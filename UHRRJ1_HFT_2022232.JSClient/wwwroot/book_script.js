let books = [];
let connection = null;
let i = 1;
getdata();
setupSignalR();

let bookIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:23125/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("bookCreated", (user, message) => {
        getdata();
    });

    connection.on("bookDeleted", (user, message) => {
        getdata();
    });

    connection.on("bookUpdated", (user, message) => {
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
    await fetch('http://localhost:23125/book')
        .then(x => x.json())
        .then(y => {
            books = y;
            console.log(y);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    books.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr>" +
            "<td>" + t.bookId + "</td>" +
            "<td>" + t.title + "</td>" +
            "<td>" + `<button type="button" onclick="remove(${t.bookId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.bookId})">Update</button>` +
            "</td>" +
            "</tr>";
    });
}

function showupdate(id) {
    document.getElementById('titletoupdate').value = books.find(t => t['bookId'] == id)['title'];
    document.getElementById('updateformdiv').style.display = 'flex';
    bookIdToUpdate = id;
}

function remove(id) {
    fetch('http://localhost:23125/book/' + id, {
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
    let name = document.getElementById('titletoupdate').value;
    fetch('http://localhost:23125/book', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: name, bookId: bookIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('title').value;
    fetch('http://localhost:23125/book', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}