using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Regular expression for email addresses (case‑insensitive)
        Regex emailRegex = new Regex(@"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}\b", RegexOptions.IgnoreCase);

        // Enable regular‑expression search
        TextSearchOptions searchOptions = new TextSearchOptions(true);

        // Absorber that searches using the email regex
        TextFragmentAbsorber absorber = new TextFragmentAbsorber(emailRegex, searchOptions);

        // Load the PDF and search each page
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                page.Accept(absorber);
            }
        }

        // Collect all matched email strings
        List<string> emails = new List<string>();
        foreach (KeyValuePair<Regex, TextFragmentCollection> entry in absorber.RegexResults)
        {
            foreach (TextFragment fragment in entry.Value)
            {
                emails.Add(fragment.Text);
            }
        }

        // Output the results
        Console.WriteLine("Found email addresses:");
        foreach (string email in emails)
        {
            Console.WriteLine(email);
        }
    }
}