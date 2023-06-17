import { HubConnectionBuilder } from "@microsoft/signalr";

const connection = new HubConnectionBuilder()
    .withUrl("http://localhost:5000/chatHub")
    .build();

connection.start()
    .then(() => {
        console.log("Bağlantı başarılı!");
        // Bağlantı başarılı olduğunda yapılacak işlemleri buraya ekleyin
    })
    .catch((error) => {
        console.log("Bağlantı hatası: ", error);
    });
