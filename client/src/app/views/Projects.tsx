import React from "react";
import Table from "../components/Table";

export default function Projects() {
    return (
        <>
            <div className="flex items-center my-6">
                <div className="w-1/2">
                    <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                        Add entry
                    </button>
                </div>
            </div>

            <Table />
        </>
    );
}
