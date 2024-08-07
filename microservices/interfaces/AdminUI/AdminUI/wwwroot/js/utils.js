export const apiRequest = async (method, url, content = null, token = null) => {
    const headers = new Headers({
        "Content-Type": "application/json"
    });

    if (token) {
        headers.append("Authorization", `Bearer ${token}`);
    }

    try {
        const response = await fetch(url, {
            method: method,
            headers: headers,
            body: content ? JSON.stringify(content) : null
        });

        const text = await response.text();
        if (!response.ok) {
            let message = text;
            const errorData = JSON.parse(text);
            message = errorData.message || text;
            throw new Error(message);
        }

        try {
            return JSON.parse(text);
        } catch (jsonError) {
            return text;
        }

    } catch (error) {
        throw new Error(error.message);
    }
};

export const put = async (url, content, token) => {
    const headers = new Headers({
        "Content-Type": "application/json"
    });

    if (token) {
        headers.append("Authorization", `Bearer ${token}`);
    }

    try {
        const response = await fetch(url, {
            method: 'PUT',
            headers: headers,
            body: JSON.stringify(content)
        });

        const text = await response.text();
        if (!response.ok) throw new Error(text);

        try {
            return JSON.parse(text);
        } catch (jsonError) {
            return text;
        }
    } catch (error) {
        throw new Error(error.message);
    }
};

export const post = async (url, content, token) => apiRequest('POST', url, content, token);
export const get = async (url, token) => apiRequest('GET', url, null, token);
export const remove = async (url, token) => apiRequest('DELETE', url, null, token);

export const buildURL = (baseURL, params) => {
    const query = Object.keys(params)
        .map(key => `${encodeURIComponent(key)}=${encodeURIComponent(params[key])}`)
        .join('&');
    return `${baseURL}?${query}`;
};