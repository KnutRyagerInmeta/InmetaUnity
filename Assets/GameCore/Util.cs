using System;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public const int AREA_WIDTH = 10;   // TODO: remove or use

    private static readonly System.Random getrandom = new System.Random();
    private static readonly object syncLock = new object();
    public static int GetRandomNumber(int min, int max)
    {
        lock (syncLock)
        { // synchronize
            return getrandom.Next(min, max);
        }
    }

    public static string FormatSeconds(int timeInSeconds)
    {
        int seconds = (timeInSeconds % 60);
        string secondsString = seconds.ToString();
        if (seconds < 10)
            secondsString = "0" + secondsString;
        return string.Concat(new string[] { (timeInSeconds / 60).ToString(), ":", secondsString });

    }

    public static string FormatCentiseconds(int frames)
    {
        int frameRate = 60; // TODO
        int seconds = (frames % frameRate) * 100 / frameRate;
        string secondsString = seconds.ToString();
        if (seconds < 10)
            secondsString = "0" + secondsString;
        return string.Concat(new string[] { (frames / frameRate).ToString(), ":", secondsString });

    }

    public static string FormatTimeFromFrames(int frames)
    {
        int frameRate = 60; // TODO
        int framePart = frames % frameRate;
        int seconds = frames / frameRate;
        int minutes = seconds / 60;
        seconds = seconds % 60;
        string frameString = framePart.ToString();
        if (framePart < 10)
            frameString = "0" + frameString;
        string secondsString = seconds.ToString();
        if (seconds < 10)
            secondsString = "0" + secondsString;
        string minuteString = minutes.ToString();
        return string.Concat(new string[] { minuteString, ":", secondsString, ":", frameString });
    }

    public const string WHITE_TAG = "<color=#FFFFFFFF>";
    public const string GREEN_TAG = "<color=#00FF00FF>";
    public const string RED_TAG = "<color=#FF0000FF>";
    public const string BLUE_TAG = "<color=#0000FFFF>";
    public const string END_TAG = "</color>";

    public const int WHITE_TEXT = 0;
    public const int RED_TEXT = 1;
    public const int GREEN_TEXT = 2;
    public const int BLUE_TEXT = 3;
    public static readonly string[] colorTags = new string[] { WHITE_TAG, RED_TAG, GREEN_TAG, BLUE_TAG };
    /// <summary>
    /// Rounds a float to 2 decimal places.
    /// Optionally, specify places
    /// </summary>
    /// <param name="val">Value to be rounded.</param>
    public static float Round(float val, int spaces = 2)
    {
        spaces = System.Math.Min(15, spaces);
        float x = 1f;
        for (int i = 0; i < spaces; ++i)
        {
            x *= 10f;
        }
        return Mathf.Round(val * x) / x;
    }

    public static float RoundNecessary(float val, int spaces = 2)
    {
        spaces = Math.Min(15, spaces);
        float x = 1f;
        for (int i = 0; i < spaces; ++i)
        {
            x *= 10f;
        }
        float absVal = Math.Abs(val);
        while ((1f / x) > absVal && x < 1e15)
            x *= 10f;
        float ans = Mathf.Round(val * x) / x;
        //if (float.IsNaN (ans) || (ans == 0f && val != 0f))
        //Debug.Log ("dat nan: " + val + "," + spaces + "," + x);
        return Mathf.Round(val * x) / x;
    }

    public static string ColorText(string colorTag, string text)
    {
        return colorTag + text + END_TAG;
    }

    public static String GetColorTagWhiteRedGreen(float number, float limit)
    {
        string colorTag = WHITE_TAG;
        if (number != limit)
            colorTag = number > limit ? GREEN_TAG : RED_TAG;
        return colorTag;
    }


    public static string RedWhiteOrGreenTextFloat(float number, float limit, bool moreIsGood = true)
    {
        string colorTag;
        if (moreIsGood)
        {
            colorTag = GetColorTagWhiteRedGreen(number, limit);
        }
        else
        {
            colorTag = GetColorTagWhiteRedGreen(limit, number);
        }
        string rounded = RoundNecessary(number).ToString();
        return ColorText(colorTag, rounded);
    }

    public static string RedWhiteOrGreenText(float number, float limit, bool moreIsGood = true)
    {
        return RedWhiteOrGreenTextFloat(number, limit, moreIsGood);
    }

    public static string RedWhiteOrGreenTextParanthesisFloat(float number, float limit, bool moreIsGood = true)
    {
        string original = RoundNecessary(limit).ToString();
        if (number == limit)
            return original;
        string colorTag, mathSymbol = number > limit ? "+" : "-";
        if (moreIsGood)
        {
            colorTag = GetColorTagWhiteRedGreen(number, limit);
        }
        else
        {
            colorTag = GetColorTagWhiteRedGreen(limit, number);

        }
        string bonus = RoundNecessary(Mathf.Abs(number - limit)).ToString();

        return original + " (" + colorTag + mathSymbol + bonus + END_TAG + ")";
    }


    public static string RedWhiteOrGreenTextParanthesis(float number, float limit, bool moreIsGood = true)
    {
        return RedWhiteOrGreenTextParanthesisFloat(number, limit, moreIsGood);
    }

    public static string GetColorTags(string msg, int color)
    {
        return colorTags[color] + msg + END_TAG;
    }

    public static Quaternion RandomYAxisAngle()
    {

        Vector3 euler = new Vector3(0, 0, 0);
        euler.y = GetRandomNumber(0, 360);
        return Quaternion.Euler(euler);
    }


    public static string DetermineNounParticle(string noun)
    {
        if (noun.Length > 0)
        {
            char c = noun[0];
            if (IsVowel(c))
                return "an";
            else if (!DetermineIsSingularNoun(noun))
                return "some";
        }
        return "a";
    }

    public static bool DetermineIsSingularNoun(string noun)
    {
        if (noun.Length > 0)
        {
            return noun[noun.Length - 1] != 's';
        }
        return true;
    }

    public static string DetermineReferToNounParticle(string noun)
    {
        if (DetermineIsSingularNoun(noun))
            return "it";
        return "them";
    }

    public static string DetermineSingularNounParticle(string noun)
    {
        if (DetermineIsSingularNoun(noun))
            return "one";
        return "some";
    }

    public static bool IsVowel(char c)
    {
        c = char.ToLower(c);
        return (c == 'a') || (c == 'e') || (c == 'i') || (c == 'o') || (c == 'u') || (c == 'y');
    }

    private static readonly DateTime baseDate = new DateTime(1970, 1, 1);
    /// <summary>
    /// Get the current time in milliseconds since 1970/1/1
    /// </summary>
    public static double GetMilliSecond()
    {
        return (DateTime.Now - baseDate).TotalMilliseconds;
    }

    /// <summary>
    /// Formats an exception to a readable state, by removing useless info such as folders from stack trace.
    /// </summary>
    public static string[] FormatExceptionReadable(Exception e)
    {
        string[] lines = e.ToString().Split(new char[] { '\n' });

        for (int i = 1; i < lines.Length; ++i)
        {
            string line = lines[i];

            string[] splitEndingOf = line.Split(new char[] { ':' });

            string[] splitBeginning = splitEndingOf[0].Split(new char[] { ' ' });

            string methodName = splitBeginning[3];

            string lineNumber = splitEndingOf[splitEndingOf.Length - 1];
            //Debug.Log ("splitEndingOf[0]: " + splitEndingOf[0]);
            //Debug.Log ("splitBeginning: " + splitBeginning[2] + "," + splitBeginning[3]);
            //Debug.Log ("methodName: " + methodName);
            line = methodName + " " + lineNumber;

            lines[i] = line;
        }

        return lines;
    }

    public static void SmallestSpanningGraph()
    {

    }
    
}

