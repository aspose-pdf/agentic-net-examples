using System;
using System.IO;

class Program
{
    static void Main()
    {
        string html = "<!DOCTYPE html>\n" +
                      "<html>\n" +
                      "<head>\n" +
                      "    <meta charset=\"utf-8\" />\n" +
                      "    <title>Form Validation Example</title>\n" +
                      "    <script>\n" +
                      "        // Checks that every element with the \"required\" attribute has a non‑empty value\n" +
                      "        function validateForm() {\n" +
                      "            // Select all required inputs, textareas and selects\n" +
                      "            var requiredElements = document.querySelectorAll('input[required], textarea[required], select[required]');\n" +
                      "            var allFilled = true;\n" +
                      "\n" +
                      "            requiredElements.forEach(function (elem) {\n" +
                      "                // Trim whitespace to treat spaces as empty\n" +
                      "                if (!elem.value.trim()) {\n" +
                      "                    allFilled = false;\n" +
                      "                }\n" +
                      "            });\n" +
                      "\n" +
                      "            // Enable the submit button only when all required fields are filled\n" +
                      "            document.getElementById('submitBtn').disabled = !allFilled;\n" +
                      "        }\n" +
                      "\n" +
                      "        // Attach validation to relevant events after the DOM is ready\n" +
                      "        document.addEventListener('DOMContentLoaded', function () {\n" +
                      "            var form = document.getElementById('myForm');\n" +
                      "\n" +
                      "            // Run validation on any input or change within the form\n" +
                      "            form.addEventListener('input', validateForm);\n" +
                      "            form.addEventListener('change', validateForm);\n" +
                      "\n" +
                      "            // Perform an initial check in case fields are pre‑filled\n" +
                      "            validateForm();\n" +
                      "        });\n" +
                      "    </script>\n" +
                      "</head>\n" +
                      "<body>\n" +
                      "    <form id=\"myForm\">\n" +
                      "        <label>\n" +
                      "            Name:\n" +
                      "            <input type=\"text\" name=\"name\" required />\n" +
                      "        </label><br />\n" +
                      "\n" +
                      "        <label>\n" +
                      "            Email:\n" +
                      "            <input type=\"email\" name=\"email\" required />\n" +
                      "        </label><br />\n" +
                      "\n" +
                      "        <label>\n" +
                      "            Message:\n" +
                      "            <textarea name=\"message\" required></textarea>\n" +
                      "        </label><br />\n" +
                      "\n" +
                      "        <button type=\"submit\" id=\"submitBtn\" disabled>Submit</button>\n" +
                      "    </form>\n" +
                      "</body>\n" +
                      "</html>";

        File.WriteAllText("index.html", html);
        Console.WriteLine("HTML file 'index.html' has been created.");
    }
}
