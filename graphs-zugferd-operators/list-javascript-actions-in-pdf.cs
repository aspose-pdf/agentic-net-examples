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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // ---------- Document‑level JavaScript ----------
            Console.WriteLine("Document‑level JavaScript actions:");
            if (doc.JavaScript != null && doc.JavaScript.Keys != null)
            {
                foreach (string key in doc.JavaScript.Keys)
                {
                    string script = doc.JavaScript[key];
                    Console.WriteLine($"- [{key}] {script}");
                }
            }

            // Document actions (Open / Close) may also contain JavaScript
            PrintIfJavascript("Open", doc.OpenAction);

            // CloseAction may not exist in older versions – use reflection and treat it as IAppointment
            var closeProp = doc.GetType().GetProperty("CloseAction");
            if (closeProp != null)
            {
                var closeAction = closeProp.GetValue(doc) as IAppointment;
                PrintIfJavascript("Close", closeAction);
            }

            // ---------- Page‑level JavaScript ----------
            Console.WriteLine("\nPage‑level JavaScript actions:");
            foreach (Page page in doc.Pages)
            {
                var pageActions = page.Actions; // PageActionCollection
                if (pageActions != null)
                {
                    PrintPageIfJavascript(page.Number, "OnOpen",  pageActions.OnOpen);
                    PrintPageIfJavascript(page.Number, "OnClose", pageActions.OnClose);
                }
            }
        }
    }

    // Helper to output a document‑level action if it is a JavaScript action
    static void PrintIfJavascript(string actionName, IAppointment action)
    {
        if (action is JavascriptAction js && !string.IsNullOrEmpty(js.Script))
        {
            Console.WriteLine($"{actionName} JavaScript: {js.Script}");
        }
    }

    // Helper to output a page‑level action if it is a JavaScript action
    static void PrintPageIfJavascript(int pageNumber, string actionName, PdfAction action)
    {
        if (action is JavascriptAction js && !string.IsNullOrEmpty(js.Script))
        {
            Console.WriteLine($"Page {pageNumber} {actionName} JavaScript: {js.Script}");
        }
    }
}
