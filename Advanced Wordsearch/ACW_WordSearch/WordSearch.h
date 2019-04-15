#pragma once
#include "Dictionary.h"
#include <vector>
#include <string>

class WordSearch {	
	// Add your code here	
	std::vector<std::string> _dictionary;
	std::vector<std::string> _foundDictionary;
	std::vector<std::string> _unfoundDictionary;
	std::vector<int> _startX;
	std::vector<int> _startY;	
	Dictionary* _advancedDictionary;
	char** _puzzleGrid;
	const char* _puzzleName = "wordsearch_grid.txt";
	const char* _dictionaryName = "dictionary.txt";
	char* _puzzleData;
	const char* _outputFile;
	int _boardSize;
	int _dictionaryVisits;
	int _gridCellsVisited;	

public:
	explicit WordSearch(const char * const filename);
	~WordSearch();
	WordSearch(const WordSearch& rhs);
	WordSearch& operator=(const WordSearch& rhs);
	void ReadSimplePuzzle();
	void ReadSimpleDictionary();
	void ReadAdvancedPuzzle();
	void ReadAdvancedDictionary();
	void SolvePuzzleSimple();
	void SolvePuzzleAdvanced() ;
	void WriteResults(const	double loadTime, const double solveTime) const;

	// Add your code here
};

