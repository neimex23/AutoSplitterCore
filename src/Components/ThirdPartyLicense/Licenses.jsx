const licenses = [
  {
    name: "MIT License",
    text: `The MIT License (MIT)

Permission is hereby granted, free of charge, to any person obtaining a copy 
of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights 
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
SOFTWARE.`,
  },
  {
    name: "GNU General Public License (GPL-3.0)",
    text: `The GNU General Public License is a free, copyleft license for software and other kinds of works.
It guarantees your freedom to share and change all versions of a program, ensuring that
it remains free software for all its users. (Complete license on LICENSE - SOULSMEMORY)`,
  },
  {
    name: "Pixabay License",
    text: `Images and Videos on Pixabay are made available under the Pixabay License.
Under the Pixabay License you are granted an irrevocable, worldwide, non-exclusive and royalty-free right
to use, download, copy, modify or adapt the Images and Videos for commercial or non-commercial purposes.`,
  },
  {
    name: "Apache License 2.0",
    text: `Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is 
distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and limitations under the License.`,
  },
];

import React from "react";

export const Licenses = () => {
  return (
    <section
      id="licenses"
      className="bg-amber-200 p-6 rounded-lg shadow  max-w-1/2 m-4">
      <h2 className="text-2xl font-bold text-gray-800 mb-4">Licenses</h2>
      {licenses.map((license, index) => (
        <article key={index} className="mb-6">
          <h3 className="text-xl font-semibold text-red-700 mb-2">
            {license.name}
          </h3>
          <pre className="bg-amber-100 p-4 rounded-md overflow-x-auto text-sm whitespace-pre-wrap">
            {license.text}
          </pre>
        </article>
      ))}
    </section>
  );
};
