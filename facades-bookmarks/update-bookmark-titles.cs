using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define proper nouns that should retain their original casing
        List<string> properNouns = new List<string> { "Aspose", "PDF", "CSharp" };

        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Extract all bookmarks from the document
            Bookmarks bookmarks = editor.ExtractBookmarks();

            foreach (Bookmark bm in bookmarks)
            {
                string originalTitle = bm.Title;
                string newTitle = ConvertToTitleCase(originalTitle, properNouns);
                if (!originalTitle.Equals(newTitle))
                {
                    editor.ModifyBookmarks(originalTitle, newTitle);
                }
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks updated and saved to '{outputPath}'.");
    }

    private static string ConvertToTitleCase(string text, List<string> properNouns)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        string[] words = text.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            string matchingProper = null;
            foreach (string pn in properNouns)
            {
                if (string.Equals(pn, word, StringComparison.OrdinalIgnoreCase))
                {
                    matchingProper = pn;
                    break;
                }
            }

            if (matchingProper != null)
            {
                words[i] = matchingProper;
            }
            else
            {
                if (word.Length > 1)
                {
                    string first = char.ToUpper(word[0]).ToString();
                    string rest = word.Substring(1).ToLower();
                    words[i] = first + rest;
                }
                else
                {
                    words[i] = word.ToUpper();
                }
            }
        }

        return string.Join(" ", words);
    }
}