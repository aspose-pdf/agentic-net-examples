using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Document‑level JavaScript collection (Document.JavaScript)
            // -----------------------------------------------------------------
            Console.WriteLine("Document‑level JavaScript scripts:");
            if (doc.JavaScript != null && doc.JavaScript.Keys.Count > 0)
            {
                // JavaScriptCollection is not enumerable – iterate via its Keys collection
                foreach (string key in doc.JavaScript.Keys)
                {
                    string script = doc.JavaScript[key];
                    Console.WriteLine($"- [{key}] {script}");
                }
            }
            else
            {
                Console.WriteLine("- none");
            }

            // -----------------------------------------------------------------
            // 2. Document actions that may contain JavaScript (Document.Actions)
            // -----------------------------------------------------------------
            DocumentActionCollection actions = doc.Actions;

            PrintActionScript("AfterPrinting",  actions.AfterPrinting);
            PrintActionScript("AfterSaving",    actions.AfterSaving);
            PrintActionScript("BeforeClosing", actions.BeforeClosing);
            PrintActionScript("BeforePrinting",actions.BeforePrinting);
            PrintActionScript("BeforeSaving",  actions.BeforeSaving);

            // -----------------------------------------------------------------
            // 3. Page‑level scripts (OnOpen / OnClose)
            // -----------------------------------------------------------------
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // PageActionCollection only exposes OnOpen and OnClose actions.
                if (page.Actions?.OnOpen != null)
                    PrintActionScript($"Page {i} OnOpen", page.Actions.OnOpen);

                if (page.Actions?.OnClose != null)
                    PrintActionScript($"Page {i} OnClose", page.Actions.OnClose);
            }
        }
    }

    // Helper to output JavaScript from a PdfAction if it is a JavascriptAction.
    static void PrintActionScript(string actionName, PdfAction action)
    {
        if (action == null) return;

        if (action is JavascriptAction js)
        {
            Console.WriteLine($"{actionName} JavaScript: {js.Script}");
        }
        else
        {
            // For non‑JavaScript actions, attempt to retrieve the ECMAScript string.
            string ecma = action.GetECMAScriptString();
            if (!string.IsNullOrEmpty(ecma))
                Console.WriteLine($"{actionName} ECMAScript: {ecma}");
        }
    }
}
