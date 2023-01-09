#pragma once
#include <string>
#include <vector>
#include <cstdint>
#include <Windows.h>

#include "addresses.h"

/* THESE TWO ADDRESSES CHANGE FREQUENTLY */
uintptr_t scheduler_address = base + 0x7502F0;
uintptr_t getstate_address = base + 0x367380;

uintptr_t jobs_start(0x134);
uintptr_t jobs_end(0x138);
uintptr_t job_name1(0x10);
uintptr_t datamodel_job(0x28);
uintptr_t script_context_job(0x130);

using getscheduler_def = uintptr_t(__cdecl*)();
getscheduler_def getscheduler = reinterpret_cast<getscheduler_def>(scheduler_address);

using getstate_def = uintptr_t(__thiscall*)(uintptr_t SC, int* state_type);
getstate_def getstate = reinterpret_cast<getstate_def>(getstate_address);

/* IGNORE JUST SCANNING */
class scanner {
	private:
		uintptr_t taskscheduler = 0;
		uintptr_t datamodel = 0;
		uintptr_t script_context = 0;

	public:
		explicit scanner();
		uintptr_t get() const;
		uintptr_t get_datamodel() const;
		uintptr_t get_script_context() const;
		uintptr_t get_global_luastate() const;
		std::vector< uintptr_t> get_jobs() const;
		uintptr_t get_job_by_name(const std::string& name) const;
		void print_jobs() const;
};

scanner::scanner() 
{
	this->taskscheduler = getscheduler();
	uintptr_t waiting_scripts_job = this->get_job_by_name("WaitingHybridScriptsJob");
	this->datamodel = *reinterpret_cast<uintptr_t*>(waiting_scripts_job + datamodel_job);
	this->script_context = *reinterpret_cast<uintptr_t*>(waiting_scripts_job + script_context_job);
}

uintptr_t scanner::get() const 
{
	return this->taskscheduler;
}

uintptr_t scanner::get_datamodel() const
{
	return this->datamodel;
}

uintptr_t scanner::get_script_context() const
{
	return this->script_context;
}

uintptr_t scanner::get_global_luastate() const 
{
	int state_type = 0;
	return getstate(this->script_context, &state_type);
}

void scanner::print_jobs() const 
{
	for (uintptr_t& job : this->get_jobs()) 
	{
		std::string * job_name = reinterpret_cast<std::string*>(job + job_name1);
	}
}

uintptr_t scanner::get_job_by_name(const std::string& name) const
{
	for (uintptr_t& job : this->get_jobs())
	{
		std::string* job_name = reinterpret_cast<std::string*>(job + job_name1);
		if (*job_name == "WaitingHybridScriptsJob")
		{
			return job;
		}
	}
	return NULL;
}

std::vector<uintptr_t> scanner::get_jobs() const
{
	std::vector<uintptr_t> jobs;
	uintptr_t* current_job = *reinterpret_cast<uintptr_t**>(this->taskscheduler + jobs_start);
	do {
		jobs.push_back(*current_job);
		current_job += 2;
	} while (current_job != *reinterpret_cast<uintptr_t**>(this->taskscheduler + jobs_end));

	return jobs;
}

