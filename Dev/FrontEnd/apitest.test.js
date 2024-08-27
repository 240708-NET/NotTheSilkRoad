const getData = async () => {
const response = await fetch("http://localhost:${process.env.PORT}")
if (!response.ok) {
    throw new Error(`Response status: ${response.status}`);
  }

const json = await response.json()

console.log(json)

}

getData();

