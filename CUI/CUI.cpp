#include <iomanip>
#include <iostream>
#include <string>
#include "CUI.h"

// ==========================================
// Progress Bar Function
// ==========================================
void drawProgressBar(size_t current, size_t total, const std::string& date, const std::string& status) {
    const int barWidth = 60;
    float progress = (float)current / total;
    int pos = (int)(barWidth * progress);

    std::cout << "\r"; // Return to start of line
    std::cout << "[";
    for (int i = 0; i < barWidth; ++i) {
        if (i < pos) std::cout << "=";
        else if (i == pos) std::cout << ">";
        else std::cout << " ";
    }
    std::cout << "] " << int(progress * 100.0) << "% "
        << "| " << current << "/" << total << " "
        << "| Date: " << std::left << std::setw(12) << date
        << "| Status: " << status;

    // Clear remaining line to prevent artifacts
    std::cout << "\033[K";
    std::cout.flush();
}