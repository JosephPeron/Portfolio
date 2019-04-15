#include "WordSearch.h"
#include "Dictionary.h"
#include <iostream>
#include <fstream>
#include <vector>
#include <string>

using namespace std;

WordSearch::WordSearch(const char * const filename) : _advancedDictionary(new Dictionary()), _puzzleGrid(nullptr), _puzzleData(nullptr), _outputFile(filename), _boardSize(0),_dictionaryVisits(0),_gridCellsVisited(0) {
	// Add your code here
	
}

WordSearch::~WordSearch() {
	// Add your code here
	delete _advancedDictionary;
	delete[] _puzzleGrid;
	delete[] _puzzleData;

}

WordSearch::WordSearch(const WordSearch& rhs) : _dictionary(rhs._dictionary), _foundDictionary(rhs._foundDictionary), _unfoundDictionary(rhs._unfoundDictionary), _startX(rhs._startX), _startY(rhs._startY), _advancedDictionary(rhs._advancedDictionary), _puzzleGrid(rhs._puzzleGrid), _puzzleName(rhs._puzzleName), _dictionaryName(rhs._dictionaryName), _puzzleData(rhs._puzzleData), _outputFile(rhs._outputFile), _boardSize(rhs._boardSize), _dictionaryVisits(rhs._dictionaryVisits), _gridCellsVisited(rhs._gridCellsVisited) {
	
}

WordSearch& WordSearch::operator= (const WordSearch& rhs) {
	if (&rhs != this) {
		_dictionary = rhs._dictionary;
		_foundDictionary = rhs._foundDictionary;
		_unfoundDictionary = rhs._unfoundDictionary;
		_advancedDictionary = rhs._advancedDictionary;
		_startX = rhs._startX;
		_startY = rhs._startY;
		_puzzleGrid = rhs._puzzleGrid;
		_puzzleName = rhs._puzzleName;
		_dictionaryName = rhs._dictionaryName;
		_puzzleData = rhs._puzzleData;
		_outputFile = rhs._outputFile;
		_dictionaryVisits = rhs._dictionaryVisits;
		_gridCellsVisited = rhs._gridCellsVisited;
		_boardSize = rhs._boardSize;
		return *this;
	}
}

void WordSearch::ReadSimplePuzzle() {
	// Add your code here
	ifstream reader(_puzzleName);
	if (reader) {
		reader >> _boardSize;

		_puzzleData = new char[_boardSize * _boardSize];
		_puzzleGrid = new char*[_boardSize];
		for (int i = 0; i < _boardSize; i++) {
			_puzzleGrid[i] = &_puzzleData[i * _boardSize];
		}

		for (int j = 0; j < _boardSize; j++) {
			for (int i = 0; i < _boardSize; i++) {
				char letter;
				reader >> letter;
				_puzzleGrid[i][j] = letter;
			}
		}
	}
	reader.close();
}

void WordSearch::ReadSimpleDictionary() {
	// Add your code here
	ifstream reader(_dictionaryName);
	if (reader) {
		while (!reader.eof()) {
			string line = "";
			reader >> line;
			_dictionary.push_back(line);
		}
	}
	reader.close();
}

void WordSearch::ReadAdvancedPuzzle() {
	// Add your code here
	cout << "Method not implented" << endl;
	ReadSimplePuzzle();
}

void WordSearch::ReadAdvancedDictionary() {
	// Add your code here
	ifstream reader(_dictionaryName);
	if (reader) {
		while (!reader.eof()) {
			string line = "";
			reader >> line;
			_dictionary.push_back(line);
			_advancedDictionary->AddWord(line);
		}
	}
	reader.close();
 }

void WordSearch::SolvePuzzleSimple() {
	// Add your code here
	for (int yaxis = 0; yaxis < _boardSize; yaxis++) {
		for (int xaxis = 0; xaxis < _boardSize; xaxis++) {
			char currentletter;
			currentletter = _puzzleGrid[xaxis][yaxis];
			for (int w = 0; w < _dictionary.size(); w++) {
				_dictionaryVisits++;
				string word = _dictionary[w];
				if (word[0] == currentletter) {
					bool wordmatch;
					for (int x = -1; x < 2; x++) {
						for (int y = -1; y < 2; y++) {
							wordmatch = true;
							for (int l = 1; l < word.length(); l++) {
								const int peekx = xaxis + (x * l);
								const int peeky = yaxis + (y * l);
								if (peeky > _boardSize - 1 ||
									peeky < 0 ||
									peekx > _boardSize - 1 ||
									peekx < 0) {
									wordmatch = false;
									break;
								}

								_gridCellsVisited++;
								if (word[l] != _puzzleGrid[peekx][peeky]) {
									wordmatch = false;
									break;
								}
							}
							if (wordmatch == true) {
								_foundDictionary.push_back(word);
								_startX.push_back(xaxis);
								_startY.push_back(yaxis);
							}
							
						}
					}
				}
			}
		}
	}
	for (int a = 0; a < _dictionary.size(); a++) {
		bool found = false;
		for (int b = 0; b < _foundDictionary.size(); b++) {
			if (_dictionary[a] == _foundDictionary[b]) {
				found = true;
			}
		}
		if (!found) {
			_unfoundDictionary.push_back(_dictionary[a]);
		}
	}

}

void WordSearch::SolvePuzzleAdvanced() {
	// Add your code here
	for (int yaxis = 0; yaxis < _boardSize; yaxis++) {
		for (int xaxis = 0; xaxis < _boardSize; xaxis++) {
			char startletter;
			startletter = _puzzleGrid[xaxis][yaxis];
			_gridCellsVisited++;
			if (_advancedDictionary->StartSearch(startletter)) {
				_dictionaryVisits++;
				for (int x = -1; x < 2; x++) {
					for (int y = -1; y < 2; y++) {
						string word;
						word.push_back(startletter);
						bool matching = true;
						int l = 1;
						while (matching)
						{
							string newWord = word;
							const int peekx = xaxis + (x * l);
							const int peeky = yaxis + (y * l);
							if (peeky > _boardSize - 1 ||
								peeky < 0 ||
								peekx > _boardSize - 1 ||
								peekx < 0) {
								matching = false;
							}
							else {
								char const currentletter = _puzzleGrid[peekx][peeky];
								_gridCellsVisited++;
								newWord += currentletter;
								matching = _advancedDictionary->ContinueSearch(currentletter);
								_dictionaryVisits++;
								l++;
							}
							if (matching) {
								word = newWord;
							}
						}
						if (_advancedDictionary->CheckEOW()) {
							_foundDictionary.push_back(word);
							_startX.push_back(xaxis);
							_startY.push_back(yaxis);						
						}
						_advancedDictionary->ResetSearch(startletter);
					}
				}
			}
		}
	}

	for (int a = 0; a < _dictionary.size(); a++) {
		bool found = false;
		for (int b = 0; b < _foundDictionary.size(); b++) {
			if (_dictionary[a] == _foundDictionary[b]) {
				found = true;
			}
		}
		if (!found) {
			_unfoundDictionary.push_back(_dictionary[a]);
		}
	}
}

void WordSearch::WriteResults(const double loadTime, const double solveTime) const {
	// Add your code here
	ofstream writer(_outputFile);

	if (writer) {
		writer << "NUMBER_OF_WORDS_MATCHED " << _foundDictionary.size() << endl;
		writer << endl;
		writer << "WORDS_MATCHED_IN_GRID" << endl;
		for (int i = 0; i < _foundDictionary.size(); i++) {
			writer << _startX[i] << " " << _startY[i] << " " << _foundDictionary[i] << endl;
		}
		writer << endl;
		writer << "WORDS_UNMATCHED_IN_GRID" << endl;
		for (int j = 0; j < _unfoundDictionary.size(); j++) {
			writer << _unfoundDictionary[j] << endl;
		}
		writer << endl;
		writer << "NUMBER_OF_GRID_CELLS_VISITED" << " " << _gridCellsVisited << endl;
		writer << endl;
		writer << "NUMBER_OF_DICTIONARY_ENTRIES_VISITED" << " " << _dictionaryVisits << endl;
		writer << endl;
		writer << "TIME_TO_POPULATE_GRID_STRUCTURE" << " " << loadTime << endl;
		writer << endl;
		writer << "TIME_TO_SOLVE_PUZZLE" << " " << solveTime << endl;
	}
}