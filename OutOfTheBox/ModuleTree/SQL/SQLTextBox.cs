using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing;

namespace OutOfTheBox.ModuleTree.SQL
{
    /* This class is a modified version of http://www.codeproject.com/Articles/10675/Enabling-syntax-highlighting-in-a-RichTextBox and is the property of Patrik Svensson */
    public class SQLTextBox : System.Windows.Forms.RichTextBox
    {
        static bool m_bPaint = true;
        string m_strLine = "";
        int m_nContentLength = 0;
        int m_nLineLength = 0;
        int m_nLineStart = 0;
        int m_nLineEnd = 0;
        string[] m_strKeywords = { "ADD", "ALL", "ALTER", "ANALYZE", "AND", "AS", "ASC", "ASENSITIVE", "BEFORE", "BETWEEN", "BIGINT", "BINARY", "BLOB", "BOTH", "BY", "CALL", "CASCADE", "CASE", "CHANGE", "CHAR", "CHARACTER", "CHECK", "COLLATE", "COLUMN", "CONDITION", "CONNECTION", "CONSTRAINT", "CONTINUE", "CONVERT", "CREATE", "CROSS", "CURRENT_DATE", "CURRENT_TIME", "CURRENT_TIMESTAMP", "CURRENT_USER", "CURSOR", "DATABASE", "DATABASES", "DAY_HOUR", "DAY_MICROSECOND", "DAY_MINUTE", "DAY_SECOND", "DEC", "DECIMAL", "DECLARE", "DEFAULT", "DELAYED", "DELETE", "DESC", "DESCRIBE", "DETERMINISTIC", "DISTINCT", "DISTINCTROW", "DIV", "DOUBLE", "DROP", "DUAL", "EACH", "ELSE", "ELSEIF", "ENCLOSED", "ESCAPED", "EXISTS", "EXIT", "EXPLAIN", "FALSE", "FETCH", "FLOAT", "FLOAT4", "FLOAT8", "FOR", "FORCE", "FOREIGN", "FROM", "FULLTEXT", "GOTO", "GRANT", "GROUP", "HAVING", "HIGH_PRIORITY", "HOUR_MICROSECOND", "HOUR_MINUTE", "HOUR_SECOND", "IF", "IGNORE", "IN", "INDEX", "INFILE", "INNER", "INOUT", "INSENSITIVE", "INSERT", "INT", "INT1", "INT2", "INT3", "INT4", "INT8", "INTEGER", "INTERVAL", "INTO", "IS", "ITERATE", "JOIN", "KEY", "KEYS", "KILL", "LABEL", "LEADING", "LEAVE", "LEFT", "LIKE", "LIMIT", "LINES", "LOAD", "LOCALTIME", "LOCALTIMESTAMP", "LOCK", "LONG", "LONGBLOB", "LONGTEXT", "LOOP", "LOW_PRIORITY", "MATCH", "MEDIUMBLOB", "MEDIUMINT", "MEDIUMTEXT", "MIDDLEINT", "MINUTE_MICROSECOND", "MINUTE_SECOND", "MOD", "MODIFIES", "NATURAL", "NOT", "NO_WRITE_TO_BINLOG", "NULL", "NUMERIC", "ON", "OPTIMIZE", "OPTION", "OPTIONALLY", "OR", "ORDER", "OUT", "OUTER", "OUTFILE", "PRECISION", "PRIMARY", "PROCEDURE", "PURGE", "READ", "READS", "REAL", "REFERENCES", "REGEXP", "RELEASE", "RENAME", "REPEAT", "REPLACE", "REQUIRE", "RESTRICT", "RETURN", "REVOKE", "RIGHT", "RLIKE", "SCHEMA", "SCHEMAS", "SECOND_MICROSECOND", "SELECT", "SENSITIVE", "SEPARATOR", "SET", "SHOW", "SMALLINT", "SONAME", "SPATIAL", "SPECIFIC", "SQL", "SQLEXCEPTION", "SQLSTATE", "SQLWARNING", "SQL_BIG_RESULT", "SQL_CALC_FOUND_ROWS", "SQL_SMALL_RESULT", "SSL", "STARTING", "STRAIGHT_JOIN", "TABLE", "TERMINATED", "THEN", "TINYBLOB", "TINYINT", "TINYTEXT", "TO", "TRAILING", "TRIGGER", "TRUE", "UNDO", "UNION", "UNIQUE", "UNLOCK", "UNSIGNED", "UPDATE", "UPGRADE", "USAGE", "USE", "USING", "UTC_DATE", "UTC_TIME", "UTC_TIMESTAMP", "VALUES", "VARBINARY", "VARCHAR", "VARCHARACTER", "VARYING", "WHEN", "WHERE", "WHILE", "WITH", "WRITE", "XOR", "YEAR_MONTH", "ZEROFILL" };
        string[] m_strSpecialKeywords = { "@@version", "@@hostname", "@@global", "@@error", "@@identity", "@@datadir" }; /* Remind me to update this with an actual list of system variables for MySQL */
        string[] m_strNoSpace = { "%", "@", "=", "{", "}", "[", "]", "*", ".", "&", "<", ">", "~", "|", "^", "-", "+", "!", "/", "(", ")" };
        int m_nCurSelection = 0;

        public SQLTextBox()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x0F)
            {
                if (m_bPaint)
                    base.WndProc(ref m);
                else
                    m.Result = IntPtr.Zero;
            }
            else
                base.WndProc(ref m);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            m_nContentLength = this.TextLength;

            int nCurrentSelectionStart = SelectionStart;
            int nCurrentSelectionLength = SelectionLength;

            m_bPaint = false;

            m_nLineStart = nCurrentSelectionStart;
            while ((m_nLineStart > 0) && (Text[m_nLineStart - 1] != '\n'))
                m_nLineStart--;
            m_nLineEnd = nCurrentSelectionStart;
            while ((m_nLineEnd < Text.Length) && (Text[m_nLineEnd] != '\n'))
                m_nLineEnd++;
            m_nLineLength = m_nLineEnd - m_nLineStart;
            m_strLine = Text.Substring(m_nLineStart, m_nLineLength);

            ProcessLine();

            m_bPaint = true;
        }

        private void ProcessLine()
        {
            int nPosition = SelectionStart;
            SelectionStart = m_nLineStart;
            SelectionLength = m_nLineLength;
            SelectionColor = Color.Black;

            ProcessWords(m_strKeywords, Color.Blue);
            ProcessRegex("\\b(?:[0-9]*\\.)?[0-9]+\\b", Color.CadetBlue);
            ProcessWordsNoSpace(m_strNoSpace, Color.DarkViolet);
            ProcessWordsNoSpace(m_strSpecialKeywords, Color.DarkViolet);
            ProcessRegex(@"`[^`\\\r\n]*?(?:\.[^\1\\\r\n]*?)*?`", Color.Gray);
            ProcessRegex(@"(""|')[^\1\\\r\n]*?(?:\.[^\1\\\r\n]*?)*?\1", Color.Brown);
            ProcessRegex("(--|#).*$", Color.Green);

            SelectionStart = nPosition;
            SelectionLength = 0;
            SelectionColor = Color.Black;

            m_nCurSelection = nPosition;
        }

        private void ProcessRegex(string strRegex, Color color)
        {
            Regex regKeywords = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match regMatch;

            for (regMatch = regKeywords.Match(m_strLine); regMatch.Success; regMatch = regMatch.NextMatch())
            {
                // Process the words
                int nStart = m_nLineStart + regMatch.Index;
                int nLenght = regMatch.Length;
                SelectionStart = nStart;
                SelectionLength = nLenght;
                SelectionColor = color;
            }
        }

        public void ProcessWords(string[] stra, Color c)
        {
            List<int[]> found = new List<int[]>();
            foreach (string value in stra)
            {
                int len = value.Length;
                int len2 = m_strLine.Length;
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("the string to find may not be empty", "value");
                for (int index = 0; ; index += value.Length)
                {
                    index = m_strLine.IndexOf(value, index, StringComparison.CurrentCultureIgnoreCase);
                    if (index == -1)
                        break;
                    if (index > 0 && m_strLine[index - 1] != ' ' && m_strLine[index - 1] != '(') continue;
                    if (index + len <= len2 - 1 && m_strLine[index + len] != ' ' && m_strLine[index + len] != ';' && m_strLine[index + len] != ')')
                        continue;
                    found.Add(new int[2] { index, len });
                }
            }

            foreach (int[] stuff in found)
            {
                int nStart = m_nLineStart + stuff[0];
                int nLenght = stuff[1];
                SelectionStart = nStart;
                SelectionLength = nLenght;
                SelectionColor = c;
            }
        }

        public void ProcessWordsNoSpace(string[] stra, Color c)
        {
            List<int[]> found = new List<int[]>();
            foreach (string value in stra)
            {
                int len = value.Length;
                int len2 = m_strLine.Length;
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("the string to find may not be empty", "value");
                for (int index = 0; ; index += value.Length)
                {
                    index = m_strLine.IndexOf(value, index, StringComparison.CurrentCultureIgnoreCase);
                    if (index == -1)
                        break;
                    found.Add(new int[2] { index, len });
                }
            }

            foreach (int[] stuff in found)
            {
                int nStart = m_nLineStart + stuff[0];
                int nLenght = stuff[1];
                SelectionStart = nStart;
                SelectionLength = nLenght;
                SelectionColor = c;
            }
        }

        /*public void CompileKeywords()
        {
            for (int i = 0; i < Settings.Keywords.Count; i++)
            {
                string strKeyword = Settings.Keywords[i];

                if (i == Settings.Keywords.Count - 1)
                    m_strKeywords += "\\b" + strKeyword + "\\b";
                else
                    m_strKeywords += "\\b" + strKeyword + "\\b|";
            }
        }*/

        public void ProcessAllLines()
        {
            m_bPaint = false;

            int nStartPos = 0;
            int i = 0;
            int nOriginalPos = SelectionStart;
            while (i < Lines.Length)
            {
                m_strLine = Lines[i];
                m_nLineStart = nStartPos;
                m_nLineEnd = m_nLineStart + m_strLine.Length;

                ProcessLine();
                i++;

                nStartPos += m_strLine.Length + 1;
            }

            m_bPaint = true;
        }
    }
}
