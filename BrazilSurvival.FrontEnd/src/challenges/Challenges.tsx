import { FormEvent, useState } from 'react';

function Challenges() {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();

        console.log('Challenge Created:', { title, description });
    };

    return (
        <div>
            <h1>Create a New Challenge</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="title">Title:</label>
                    <input
                        type="text"
                        id="title"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                    />
                </div>
                <div>
                    <label htmlFor="description">Description:</label>
                    <textarea
                        id="description"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                    />
                </div>
                <button type="submit">Create Challenge</button>
            </form>
        </div>
    );
};

export default Challenges;