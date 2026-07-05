using System;
using System.IO;
using Aspose.Pdf;

class ListJavaScriptActions
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // ----- Document‑level JavaScript -----
            // JavaScriptCollection is not enumerable; iterate via its Keys collection.
            if (doc.JavaScript != null && doc.JavaScript.Keys.Count > 0)
            {
                foreach (string key in doc.JavaScript.Keys)
                {
                    string script = doc.JavaScript[key];
                    Console.WriteLine($"Document‑level script (key: '{key}'):");
                    Console.WriteLine(script);
                    Console.WriteLine(new string('-', 40));
                }
            }
            else
            {
                Console.WriteLine("No document‑level JavaScript found.");
            }

            // ----- Page‑level JavaScript -----
            foreach (Page page in doc.Pages)
            {
                // OnOpen action – may be a JavaScriptAction (use runtime type check to avoid the missing namespace)
                var onOpen = page.Actions?.OnOpen;
                if (onOpen != null && onOpen.GetType().Name == "JavaScriptAction")
                {
                    // Cast to dynamic to access the JavaScript property at runtime
                    dynamic jsAction = onOpen;
                    Console.WriteLine($"Page {page.Number} OnOpen script:");
                    Console.WriteLine((string)jsAction.JavaScript);
                    Console.WriteLine(new string('-', 40));
                }

                // OnClose action – may be a JavaScriptAction
                var onClose = page.Actions?.OnClose;
                if (onClose != null && onClose.GetType().Name == "JavaScriptAction")
                {
                    dynamic jsAction = onClose;
                    Console.WriteLine($"Page {page.Number} OnClose script:");
                    Console.WriteLine((string)jsAction.JavaScript);
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
    }
}
