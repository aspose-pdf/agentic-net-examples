using System;
using System.IO;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Paths for the source XML and the resulting XML
        const string inputXmlPath = "input.xml";
        const string outputXmlPath = "output.xml";

        // The HTML content to be inserted; using CDATA preserves markup characters
        const string htmlContent = "<p>Hello <b>World</b></p>";

        // Verify that the source XML file exists
        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"Source XML not found: {inputXmlPath}");
            return;
        }

        // Load the existing XML document
        XDocument xmlDoc = XDocument.Load(inputXmlPath);

        // Create a new <HtmlFragment> element containing the HTML as CDATA
        XElement htmlFragmentElement = new XElement("HtmlFragment",
            new XCData(htmlContent));

        // Insert the new element into the document.
        // Here we add it as the last child of the root element,
        // but any other insertion point can be used (e.g., before/after a specific node).
        xmlDoc.Root.Add(htmlFragmentElement);

        // Save the modified XML to the output path
        xmlDoc.Save(outputXmlPath);

        Console.WriteLine($"HtmlFragment inserted successfully. Output saved to '{outputXmlPath}'.");
    }
}