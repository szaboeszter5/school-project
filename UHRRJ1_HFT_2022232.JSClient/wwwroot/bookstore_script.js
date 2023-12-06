let bookstores = [];
let connection = null;
let i = 1;
getdata();
setupSignalR();

let bookstoreIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:23125/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("bookstoreCreated", (user, message) => {
        getdata();
    });

    connection.on("bookstoreDeleted", (user, message) => {
        getdata();
    });

    connection.on("bookstoreUpdated", (user, message) => {
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
    await fetch('http://localhost:23125/BookStore')
        .then(x => x.json())
        .then(y => {
            bookstores = y;
            console.log(y);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    bookstores.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr>" +
        "<td>" + t.bookStoreId + "</td>" +
        "<td>" + t.bookStoreName + "</td>" +
        "<td>" + `<button type="button" onclick="remove(${t.bookStoreId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.bookStoreId})">Update</button>` +
            "</td>" +
            "</tr>";
    });
}

function showupdate(id) {
    document.getElementById('BookStorenametoupdate').value = bookstores.find(t => t['bookStoreId'] == id)['bookStoreName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    bookstoreIdToUpdate = id;
}

function remove(id) {
    fetch('http://localhost:23125/BookStore/' + id, {
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
    let name = document.getElementById('BookStorenametoupdate').value;
    fetch('http://localhost:23125/BookStore', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { bookstoreName: name, bookstoreId: bookstoreIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('bookStoreName').value;
    fetch('http://localhost:23125/BookStore', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { bookstoreName: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}
