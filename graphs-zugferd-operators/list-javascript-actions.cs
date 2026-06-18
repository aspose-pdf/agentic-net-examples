using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace ListJavascriptActionsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with a document‑level and a page‑level JavaScript action.
            using (Document createDoc = new Document())
            {
                // Add a single page (evaluation mode allows up to 4 pages).
                Page page = createDoc.Pages.Add();

                // Document‑level JavaScript action (executed after the document is opened).
                // Use the OpenAction property – the JavaScriptCollection does not support Add().
                createDoc.OpenAction = new JavascriptAction("app.alert('Document opened');");

                // Page‑level JavaScript action (executed after the page is opened).
                JavascriptAction pageAction = new JavascriptAction("app.alert('Page 1 opened');");
                page.Actions.OnOpen = pageAction;

                // Save the sample PDF.
                createDoc.Save("sample.pdf");
            }

            // Step 2: Load the PDF and enumerate all JavaScript actions.
            using (Document loadDoc = new Document("sample.pdf"))
            {
                Console.WriteLine("--- Document‑level JavaScript actions ---");
                if (loadDoc.OpenAction != null && loadDoc.OpenAction is JavascriptAction)
                {
                    JavascriptAction docJs = (JavascriptAction)loadDoc.OpenAction;
                    Console.WriteLine($"OpenAction: {docJs.Script}");
                }
                else
                {
                    Console.WriteLine("No document‑level JavaScript actions found.");
                }

                Console.WriteLine("--- Page‑level JavaScript actions ---");
                for (int pageIndex = 1; pageIndex <= loadDoc.Pages.Count; pageIndex++)
                {
                    Page currentPage = loadDoc.Pages[pageIndex];
                    if (currentPage.Actions.OnOpen != null && currentPage.Actions.OnOpen is JavascriptAction)
                    {
                        JavascriptAction js = (JavascriptAction)currentPage.Actions.OnOpen;
                        Console.WriteLine($"Page {pageIndex} OnOpen: {js.Script}");
                    }
                    if (currentPage.Actions.OnClose != null && currentPage.Actions.OnClose is JavascriptAction)
                    {
                        JavascriptAction js = (JavascriptAction)currentPage.Actions.OnClose;
                        Console.WriteLine($"Page {pageIndex} OnClose: {js.Script}");
                    }
                }
            }
        }
    }
}
