#include <iostream>
#include <string>
#include <vector>
#include <windows.h>
#include <sstream>
#include <algorithm>
#include <fstream>
#include "assignment.h"
#include "answers.h"
#include "config.h"
#include "date/date.h"

void print_usage(const char *progName)
{
    std::cout << "Usage: " << progName << " [-f | -nf <date1,date2,...>] [-a] [-e]" << std::endl;
    std::cout << " -f Use date.txt (default, one date per line or range)" << std::endl;
    std::cout << " -nf Use comma-separated date list or range as argument (e.g. -nf2024-06-01,2024-06-02/2024-06-05)" << std::endl;
    std::cout << " -a Random answer mode (do not fetch correct answers)" << std::endl;
    std::cout << " -e Disable extra read (do not call submitExtraRead)" << std::endl;
}

std::vector<std::string> split_dates(const std::string &s)
{
    std::vector<std::string> result;
    std::stringstream ss(s);
    std::string item;
    while (std::getline(ss, item, ','))
    {
        item.erase(std::remove_if(item.begin(), item.end(), ::isspace), item.end());
        if (!item.empty())
            result.push_back(item);
    }
    return result;
}

int main(int argc, char *argv[])
{
    std::ifstream cookieFile("cookies_student.txt");
    bool hasAccessId = false;
    if (cookieFile.is_open())
    {
        std::string line;
        while (std::getline(cookieFile, line))
        {
            if (line.find("access.id") != std::string::npos)
            {
                hasAccessId = true;
                break;
            }
        }
        cookieFile.close();
    }
    if (!hasAccessId)
    {
        return 0;
    }

    SetConsoleOutputCP(65001);
    bool useFile = true;
    bool useArg = false;
    bool randomAnswerMode = false;
    bool enableExtraRead = true;
    std::vector<std::string> dateList;
    std::string argDates;

    // Parse arguments
    for (int i = 1; i < argc; ++i)
    {
        std::string arg = argv[i];
        if (arg == "-f")
        {
            if (useArg)
            {
                std::cout << "[Error] -f and -nf cannot be used together." << std::endl;
                print_usage(argv[0]);
                return 1;
            }
            useFile = true;
        }
        else if (arg == "-nf")
        {
            if (!useFile)
            {
                std::cout << "[Error] -f and -nf cannot be used together." << std::endl;
                print_usage(argv[0]);
                return 1;
            }
            useFile = false;
            useArg = true;
            if (i + 1 < argc)
            {
                argDates = argv[++i];
            }
            else
            {
                std::cout << "[Error] -nf requires a comma-separated date list or range." << std::endl;
                print_usage(argv[0]);
                return 1;
            }
        }
        else if (arg == "-a")
        {
            randomAnswerMode = true;
        }
        else if (arg == "-e")
        {
            enableExtraRead = false;
        }
        else if (arg == "-h" || arg == "--help")
        {
            print_usage(argv[0]);
            return 0;
        }
        else
        {
            std::cout << "[Error] Unknown argument: " << arg << std::endl;
            print_usage(argv[0]);
            return 1;
        }
    }

    if (useFile)
    {
        dateList = loadDatesFromFile("date.txt");
    }
    else if (useArg)
    {
        std::vector<std::string> lines = split_dates(argDates);
        dateList = loadDatesFromLines(lines);
    }

    if (dateList.empty())
    {
        LOG << "[Error] No valid dates found!" << std::endl;
        return 1;
    }

    LOG << "[System] Loaded " << dateList.size() << " tasks. Starting..." << std::endl;
    for (size_t i = 0; i < dateList.size(); ++i)
    {
        std::string TARGET_DATE = dateList[i];
        LOG << "\n[Task " << (i + 1) << "/" << dateList.size() << "] Processing Date: " << TARGET_DATE << std::endl;
        std::string assignmentId, level;
        LOG << "[Step] Fetching ID..." << std::endl;
        if (!fetchAssignmentId(TARGET_DATE, assignmentId, level))
        {
            LOG << "[Skip] Assignment not found for date: " << TARGET_DATE << std::endl;
            continue;
        }
        QuizInfo finalInfo;
        if (randomAnswerMode)
        {
            LOG << "[Step] Generating random answers..." << std::endl;
            if (!fetchQuestions(assignmentId, level, finalInfo))
            {
                LOG << "[Error] Failed to fetch questions for date: " << TARGET_DATE << std::endl;
                continue;
            }
        }
        else
        {
            LOG << "[Step] Fetching Ans..." << std::endl;
            if (!fetchAnswers(assignmentId, level, finalInfo))
            {
                LOG << "[Error] Failed to parse answers for date: " << TARGET_DATE << std::endl;
                continue;
            }
        }
        LOG << "[Step] Submitting..." << std::endl;
        if (submitAnswers(finalInfo, TARGET_DATE))
        {
            LOG << "[Success] Task completed for: " << TARGET_DATE << std::endl;
            if (enableExtraRead)
                submitExtraRead(finalInfo);
        }
        else
        {
            LOG << "[Warning] Submission response abnormal: " << TARGET_DATE << std::endl;
        }
    }
    LOG << "\nAll tasks completed. Press Enter to exit." << std::endl;
    std::cin.get();
    return 0;
}