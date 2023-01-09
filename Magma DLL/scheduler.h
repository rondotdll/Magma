#pragma once
#include <string>
#include <vector>
#include <cstdint>
#include <Windows.h>

#include "addresses.h"

using getscheduler_def = uintptr_t(__cdecl*)();
getscheduler_def getscheduler = reinterpret_cast<getscheduler_def>(scheduler_address);

using getstate_def = uintptr_t(__thiscall*)(uintptr_t SC, int* state_type);
getstate_def getstate = reinterpret_cast<getstate_def>(getstate_address);

/* IGNORE JUST SCANNING */
class scheduler {
private:
	uintptr_t taskscheduler = 0;
	uintptr_t datamodel = 0;
	uintptr_t script_context = 0;

public:
	explicit scheduler();
	uintptr_t get() const;
	uintptr_t get_datamodel() const;
	uintptr_t get_script_context() const;
	uintptr_t get_global_luastate() const;
	std::vector< uintptr_t> get_jobs() const;
	uintptr_t get_job_by_name(const std::string& name) const;
	void print_jobs() const;
};

scheduler::scheduler()
{
	this->taskscheduler = getscheduler();
	uintptr_t waiting_scripts_job = this->get_job_by_name("WaitingHybridScriptsJob");
	this->datamodel = *reinterpret_cast<uintptr_t*>(waiting_scripts_job + datamodel_job);
	this->script_context = *reinterpret_cast<uintptr_t*>(waiting_scripts_job + script_context_job);
}

uintptr_t scheduler::get() const
{
	return this->taskscheduler;
}

uintptr_t scheduler::get_datamodel() const
{
	return this->datamodel;
}

uintptr_t scheduler::get_script_context() const
{
	return this->script_context;
}

uintptr_t scheduler::get_global_luastate() const
{
	int state_type = 0;
	return getstate(this->script_context, &state_type);
}

void scheduler::print_jobs() const
{
	for (uintptr_t& job : this->get_jobs())
	{
		std::string* job_name = reinterpret_cast<std::string*>(job + job_name1);
	}
}

uintptr_t scheduler::get_job_by_name(const std::string& name) const
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

std::vector<uintptr_t> scheduler::get_jobs() const
{
	std::vector<uintptr_t> jobs;
	uintptr_t* current_job = *reinterpret_cast<uintptr_t**>(this->taskscheduler + jobs_start);
	do {
		jobs.push_back(*current_job);
		current_job += 2;
	} while (current_job != *reinterpret_cast<uintptr_t**>(this->taskscheduler + jobs_end));

	return jobs;
}

