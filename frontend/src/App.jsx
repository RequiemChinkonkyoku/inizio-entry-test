import React from "react";

import { useState } from "react";
import axios from "./axiosConfig";

function App() {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState([]);
  const [loading, setLoading] = useState(false);

  const handleSearch = async () => {
    if (!query.trim()) {
      alert("Please enter a keyword.");
      return;
    }

    setLoading(true);
    setResults([]);

    try {
      const res = await axios.get("/api/GoogleScrape", {
        params: { query },
      });

      setResults(res.data);
    } catch (err) {
      console.error("Search failed:", err);
      alert("Failed to fetch results.");
    } finally {
      setLoading(false);
    }
  };

  const handleDownloadJson = () => {
    const blob = new Blob([JSON.stringify(results, null, 2)], {
      type: "application/json",
    });
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = `results-${query}.json`;
    a.click();
    URL.revokeObjectURL(url);
  };

  return (
    <div style={{ fontFamily: "Arial", padding: "20px" }}>
      <h1>Google Scraper (SerpAPI)</h1>

      <input
        type="text"
        value={query}
        onChange={(e) => setQuery(e.target.value)}
        placeholder="Enter keyword..."
        style={{ width: "300px", padding: "8px" }}
      />
      <button
        onClick={handleSearch}
        style={{ padding: "8px 12px", marginLeft: "8px" }}
      >
        Search
      </button>

      {loading && <p>Loading...</p>}

      <ul style={{ marginTop: "20px" }}>
        {results.length === 0 && !loading && <li>No results found.</li>}
        {results.map((item, idx) => (
          <li key={idx} style={{ marginBottom: "10px" }}>
            <a href={item.link} target="_blank" rel="noopener noreferrer">
              {item.title}
            </a>
          </li>
        ))}
      </ul>
      {results.length > 0 && (
        <button
          onClick={handleDownloadJson}
          style={{ marginTop: "10px", padding: "8px 12px" }}
        >
          Download JSON
        </button>
      )}
    </div>
  );
}

export default App;
