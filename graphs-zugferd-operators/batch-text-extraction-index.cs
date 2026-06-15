using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace BatchTextExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create sample PDFs (self‑contained example)
            CreateSamplePdf("sample1.pdf", "First document page one.", "First document page two.");
            CreateSamplePdf("sample2.pdf", "Second document page one.", "Second document page two.");

            // List of PDF files to process (evaluation mode limits to 4 items)
            List<string> pdfFiles = new List<string>();
            pdfFiles.Add("sample1.pdf");
            pdfFiles.Add("sample2.pdf");

            // Simple in‑memory searchable index: keyword -> list of PDF file names
            Dictionary<string, List<string>> index = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            // Extract text from each PDF and populate the index
            foreach (string file in pdfFiles)
            {
                using (Document doc = new Document(file))
                {
                    TextAbsorber absorber = new TextAbsorber();
                    absorber.Visit(doc);

                    string extractedText = absorber.Text;

                    // Tokenize by whitespace and add each word to the index
                    string[] words = extractedText.Split(new char[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string word in words)
                    {
                        if (!index.ContainsKey(word))
                        {
                            index[word] = new List<string>();
                        }
                        // Limit each keyword list to 4 entries (evaluation restriction)
                        if (index[word].Count < 4 && !index[word].Contains(file))
                        {
                            index[word].Add(file);
                        }
                    }
                }
            }

            // Demonstrate a quick search
            string searchTerm = "First";
            if (index.ContainsKey(searchTerm))
            {
                Console.WriteLine("Documents containing the term '" + searchTerm + "':");
                foreach (string docName in index[searchTerm])
                {
                    Console.WriteLine("- " + docName);
                }
            }
            else
            {
                Console.WriteLine("No documents contain the term '" + searchTerm + "'.");
            }
        }

        private static void CreateSamplePdf(string fileName, string page1Text, string page2Text)
        {
            using (Document doc = new Document())
            {
                // First page
                Page page1 = doc.Pages.Add();
                TextFragment tf1 = new TextFragment(page1Text);
                page1.Paragraphs.Add(tf1);

                // Second page
                Page page2 = doc.Pages.Add();
                TextFragment tf2 = new TextFragment(page2Text);
                page2.Paragraphs.Add(tf2);

                doc.Save(fileName);
            }
        }
    }
}
