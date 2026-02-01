#include "date.h"
#include <fstream>
#include <sstream>
#include <iomanip>
#include <ctime>

static bool isWeekend(std::time_t t) {
    std::tm tm;
    localtime_s(&tm, &t);
    // tm_wday: 0 = Sunday, 6 = Saturday
    return (tm.tm_wday == 0 || tm.tm_wday == 6);
}

static std::time_t stringToTime(const std::string& dateStr) {
    std::tm tm = {};
    std::istringstream ss(dateStr);
    ss >> std::get_time(&tm, "%Y-%m-%d");
    if (ss.fail()) return (std::time_t)-1;
    return std::mktime(&tm);
}

static std::string timeToString(std::time_t t) {
    std::tm tm;
    localtime_s(&tm, &t);
    char buf[11];
    std::strftime(buf, sizeof(buf), "%Y-%m-%d", &tm);
    return std::string(buf);
}

static void processDateLine(const std::string& line, std::vector<std::string>& dates) {
    size_t slashPos = line.find('/');

    if (slashPos != std::string::npos) {
        std::string startStr = line.substr(0, slashPos);
        std::string endStr = line.substr(slashPos + 1);

        std::time_t currentT = stringToTime(startStr);
        std::time_t endT = stringToTime(endStr);

        if (currentT == (std::time_t)-1 || endT == (std::time_t)-1) return;

        while (currentT <= endT) {
            if (!isWeekend(currentT)) {
                dates.push_back(timeToString(currentT));
            }
            currentT += 86400; // +1 Day
        }
    }
    else {
        std::time_t t = stringToTime(line);
        if (t != (std::time_t)-1) {
            if (!isWeekend(t)) {
                dates.push_back(line);
            }
        }
    }
}

std::vector<std::string> loadDatesFromFile(const std::string& filename) {
    std::vector<std::string> dates;
    std::ifstream file(filename);
    if (file.is_open()) {
        std::string line;
        while (std::getline(file, line)) {
            line.erase(0, line.find_first_not_of(" \t\n\r"));
            line.erase(line.find_last_not_of(" \t\n\r") + 1);

            if (!line.empty()) {
                processDateLine(line, dates);
            }
        }
        file.close();
    }
    return dates;
}