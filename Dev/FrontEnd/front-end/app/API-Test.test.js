const getCategories = async () => {
    const response = await fetch("http://localhost:${process.env.PORT}/category");
    const data = await response.json();
    console.log(data);
};

getCategories();