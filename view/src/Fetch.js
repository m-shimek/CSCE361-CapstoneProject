import React, { useEffect, useState } from "react";

const App = () => {
  const [advice, setAdvice] = useState("");

  useEffect(() => {

    const url = "https://localhost<3001>";

    const fetchData = async () => {
      try {
        const response = await fetch(url);
        const json = await response.json();
        console.log(json.slip.advice);
        setAdvice(json.slip.advice);
      } catch (error) {
        console.log("error", error);
      }
    };

    fetchData();
  }, []);
  return <div>{advice}</div>; // Added a return statement to render the advice


};

export default App;

