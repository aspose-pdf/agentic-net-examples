using System;
using System.IO;
using System.Text;
using System.Net;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "viewer_preferences.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF using the high‑level Document API (available in all Aspose.Pdf versions)
        Document pdfDoc = new Document(inputPdf);

        // Access ViewerPreferences via dynamic to stay compatible with older library versions
        dynamic prefs = null;
        try
        {
            prefs = ((dynamic)pdfDoc).ViewerPreferences;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            return;
        }

        // Build a simple HTML page that lists the preferences
        StringBuilder html = new StringBuilder();
        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html lang=\"en\">");
        html.AppendLine("<head><meta charset=\"UTF-8\"><title>Viewer Preferences</title></head>");
        html.AppendLine("<body>");
        html.AppendLine("<h1>PDF Viewer Preferences</h1>");
        html.AppendLine("<table border=\"1\" cellpadding=\"5\" cellspacing=\"0\">");
        html.AppendLine("<tr><th>Setting</th><th>Value</th></tr>");

        // Helper to add a table row with HTML‑escaped values
        void AddRow(string name, string value)
        {
            html.AppendLine($"<tr><td>{WebUtility.HtmlEncode(name)}</td><td>{WebUtility.HtmlEncode(value)}</td></tr>");
        }

        // Populate rows – all accesses are dynamic, so they compile even if a particular property is missing at runtime
        AddRow("HideToolbar", prefs.HideToolbar?.ToString() ?? "null");
        AddRow("HideMenubar", prefs.HideMenubar?.ToString() ?? "null");
        AddRow("HideWindowUI", prefs.HideWindowUI?.ToString() ?? "null");
        AddRow("FitWindow", prefs.FitWindow?.ToString() ?? "null");
        AddRow("CenterWindow", prefs.CenterWindow?.ToString() ?? "null");
        AddRow("DisplayDocTitle", prefs.DisplayDocTitle?.ToString() ?? "null");
        AddRow("PrintScaling", prefs.PrintScaling?.ToString() ?? "null");
        AddRow("Duplex", prefs.Duplex?.ToString() ?? "null");
        AddRow("PickTrayByPDFSize", prefs.PickTrayByPDFSize?.ToString() ?? "null");
        AddRow("PrintPageRange", prefs.PrintPageRange?.ToString() ?? "null");
        AddRow("PrintPageMode", prefs.PrintPageMode?.ToString() ?? "null");
        AddRow("PrintPageOrientation", prefs.PrintPageOrientation?.ToString() ?? "null");
        AddRow("PrintPageScaling", prefs.PrintPageScaling?.ToString() ?? "null");

        html.AppendLine("</table>");
        html.AppendLine("</body>");
        html.AppendLine("</html>");

        // Write the HTML content to a file
        File.WriteAllText(outputHtml, html.ToString());

        Console.WriteLine($"Viewer preferences HTML saved to '{outputHtml}'.");
    }
}
