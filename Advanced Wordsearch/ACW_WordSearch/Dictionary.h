#pragma once
#include "Cell.h"
#include <list>
#include <string>

class Dictionary {

	std::list<Cell*> _firstLetters;
	bool _searching = false;
	Cell* _currentCell;

public:
	explicit Dictionary();
	~Dictionary();
	Dictionary(const Dictionary &rhs);
	Dictionary& Dictionary::operator=(const Dictionary& b);
	void Dictionary::AddWord(std::string& word);	
	bool Dictionary::StartSearch(char letter);
	bool Dictionary::ContinueSearch(char letter);
	void Dictionary::ResetSearch(char letter);
	void Dictionary::ResetBool() { _searching = true; };
	bool Dictionary::CheckEOW() const { return _currentCell->GetEOW(); };
	Cell* Dictionary::CreateWordOrGetWord(char letter);
};