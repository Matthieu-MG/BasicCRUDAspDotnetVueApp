const URL = "http://localhost:5206" // "https://localhost:7206"

async function RequestBackendRoute(method, route, data = null)
{
    const options = {
        method : method,
        headers :
        {
            'Content-Type': 'application/json',
        },
        body: data ? JSON.stringify(data) : null
    };

    try {
        const response = await fetch(`${URL}/${route}`, options);
        if(!response.ok)
        {
            throw new Error(`HTTP error: Status: ${response.status}`);
        }

        if(response.headers.get('content-type'))
        {
            return await response.json();
        }
    }
    catch (error) {
        console.log(error);
        throw error;
    }
}

export {RequestBackendRoute, URL}