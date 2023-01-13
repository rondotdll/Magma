#include "scheduler.h"

/* Some friendly documentation from your pals @ S7 Development
 *
 * You're probably wondering "Wtf does the scheduler do?"
 *
 * The scheduler is the native Roblox class which is in charge of handling all
 * client events, from detecting player input to updating physics simulations.
 * The task scheduler is a vital part of a Roblox exploit, as it is what we feed
 * our compiled LuaU to be executed.
 *
 * Throughout this file there are a couple more comments explaining what everything
 * actually is.
 *
 * For more info on Roblox's Task Scheduler click the link below:
 * https://create.roblox.com/docs/optimization/microprofiler/task-scheduler
 */


Scheduler::Scheduler(AddressContext addressContext)
{
    this->addressContext = addressContext;
    this->getscheduler = reinterpret_cast<getscheduler_def>(addressContext.scheduler_address);
    this->getstate = reinterpret_cast<getstate_def>(addressContext.getstate_address);
    printf("enter getscheduler\n");
    this->taskscheduler = getscheduler();
    printf("exit getscheduler\n");


    printf("enter getjobs\n");
    uintptr_t waiting_scripts_job = this->get_job_by_name("WaitingHybridScriptsJob");
    printf("exit getjobs\n");
    this->datamodel = waiting_scripts_job + datamodel_job;
    this->script_context = waiting_scripts_job + script_context_job;
}

uintptr_t Scheduler::get() const
{
    // Returns privatised value*
    /* ensures taskscheduler is readonly */
    return this->taskscheduler;
}

uintptr_t Scheduler::get_datamodel() const
{
    // Returns privatised value*
    /* ensures datamodel is readonly */
    return this->datamodel;
}

uintptr_t Scheduler::get_script_context() const
{
    // Returns privatised value*
    /* ensures script_context is readonly */
    return this->script_context;
}

uintptr_t Scheduler::get_global_luastate() const
{
    int state_type = 0;
    return getstate(this->script_context, &state_type);
}

void Scheduler::print_jobs() const
{
    for (uintptr_t& job : this->get_jobs())
    {
        std::string job_name = *(std::string*)(job + job_name1);
    }
}

uintptr_t Scheduler::get_job_by_name(const std::string& name) const
{
    for (uintptr_t& job : this->get_jobs())
    {
        printf("found job\n");
        std::string job_name = *(std::string*)(job + job_name1);
        printf("%s\n", job_name.c_str());
        if (job_name == "WaitingHybridScriptsJob")
        {
            printf("returned the job\n");
            return job;
        }
    }
    printf("returned null\n");
    return NULL;
}

std::vector<uintptr_t> Scheduler::get_jobs() const
{
    std::vector<uintptr_t> jobs;
    uintptr_t* current_job = (uintptr_t*)this->taskscheduler + jobs_start;
    do {
        printf("PUSH JOB\n");
        jobs.push_back(*current_job);
        current_job += 2;
    } while (current_job < (uintptr_t*)this->taskscheduler + jobs_end);

    return jobs;
}

//std::vector<uintptr_t> Scheduler::get_jobs() const
//{
//    std::vector<uintptr_t> jobs;
//    uintptr_t* current_job = *reinterpret_cast<uintptr_t**>(this->taskscheduler + jobs_start);
//    do {
//        jobs.push_back(*current_job);
//        current_job += 2;
//    } while (current_job != *reinterpret_cast<uintptr_t**>(this->taskscheduler + jobs_end));
//
//    return jobs;
//}
