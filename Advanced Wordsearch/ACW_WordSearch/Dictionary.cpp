#include "Dictionary.h"
#include "Cell.h"
#include <string>

using namespace std;

Dictionary::Dictionary() : _searching(false), _currentCell() {
}

Dictionary::~Dictionary() {
	//loop through all first letter and children of first letter and delete
	try {
		for (Cell* const child : _firstLetters) {
			delete child;
		}
	}
	catch (exception& e) {

	}
}

Dictionary::Dictionary(const Dictionary &rhs) : _firstLetters(rhs._firstLetters), _searching(rhs._searching), _currentCell(rhs._currentCell) {

}

Dictionary& Dictionary::operator=(const Dictionary& rhs) {
	if (&rhs != this) {
		_firstLetters = rhs._firstLetters;
		_searching = rhs._searching;
		_currentCell = rhs._currentCell;
		return *this;
	}
}

void Dictionary::AddWord(string& word) {	
	Cell* currentCell = CreateWordOrGetWord(word[0]);
	for (int i = 1; i < word.size(); i++) {
		Cell* const nextCell = currentCell->CreateLetterOrGetLetter(word[i]);
		currentCell = nextCell;
	}
	currentCell->SetEndBool();
}

bool Dictionary::StartSearch(char const letter) {
	_searching = true;
	for (Cell* const searchCell : _firstLetters) {
		if (searchCell->GetLetter() == letter) {
			_currentCell = searchCell;
			return true;
		}
	}
	_searching = false;
	return false;
}

bool Dictionary::ContinueSearch(char const letter) {
	if (_searching == true) {
		for (Cell* const checkCell : _currentCell->GetCellList()) {
			if (checkCell->GetLetter() == letter) {
				_currentCell = checkCell;
				return true;
			}			
		}
		_searching = false;
		return false;
	}
}

void Dictionary::ResetSearch(char const letter) {
	for (Cell* const resetCell : _firstLetters) {
		if (resetCell->GetLetter() == letter) {
			_currentCell = resetCell;
		}
	}
	ResetBool();
}

Cell* Dictionary::CreateWordOrGetWord(char const letter) {
	bool letterfound = false;
	if (_firstLetters.size() > 0) {
		for (Cell* const currentFirstLetter : _firstLetters) {
			if (currentFirstLetter->GetLetter() == letter) {
				letterfound = true;
				return currentFirstLetter;
			}
		}	
	}
	if (letterfound == false) {
		Cell* const newCell = new Cell(letter);
		_firstLetters.push_back(newCell);
		return newCell;
	}
	else {
		Cell* const newCell = new Cell(letter);
		_firstLetters.push_back(newCell);
		return newCell;
	}
}

